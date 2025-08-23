alter TRIGGER trg_AfterInsertUpdate_Results
ON Results
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- ‰Œ «— √Ê· ”Ã· „‰ «·‹ inserted (Ì„ﬂ‰  Ê”Ì⁄ ·œ⁄„ ⁄œ… ”Ã·« )
    DECLARE @StudentID INT, @ExamID INT;
    SELECT TOP 1 @StudentID = i.StudentID, @ExamID = i.ExamID FROM inserted i;

    -- Ã·» »Ì«‰«  «·«„ Õ«‰ «·„— »ÿ
    DECLARE @SubjectID INT, @StageID INT, @AcademicYearID INT, @ExamPeriodID INT, @ExamTypeID INT;
    SELECT 
        @SubjectID = e.SubjectID,
        @StageID = s.StageID,
        @AcademicYearID = ep.AcademicYearID,
        @ExamPeriodID = ep.ExamPeriodID,
        @ExamTypeID = ep.ExamTypeID
    FROM Exams e
    INNER JOIN Subjects s ON e.SubjectID = s.SubjectID
    INNER JOIN ExamPeriods ep ON e.ExamPeriodID = ep.ExamPeriodID
    WHERE e.ExamID = @ExamID;

    -- Õ”«» ‰ﬁ«ÿ «·Õ÷Ê— (20%)
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    SELECT @AttendancePoints = 
        (COUNT(CASE WHEN Status = 'Present' THEN 1 END) * 20.0) /
        NULLIF(COUNT(*),0)
    FROM StudentAttendance
    WHERE StudentID = @StudentID
      AND AttendanceDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                             AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @AttendancePoints IS NULL SET @AttendancePoints = 0;

    -- Õ”«» ‰ﬁ«ÿ «·«‰÷»«ÿ (40%)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;
    SELECT @DisciplinePoints = AVG(CAST(Points AS DECIMAL(5,2))) * 0.4
    FROM Discipline
    WHERE StudentID = @StudentID
      AND DisciplineDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                              AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @DisciplinePoints IS NULL SET @DisciplinePoints = 0;

    -- Õ”«» ‰ﬁ«ÿ «·«„ Õ«‰ (40%)
    DECLARE @ExamPoints DECIMAL(5,2) = 0;
    SELECT @ExamPoints = 
        (SUM(ISNULL(r.Score,0)) * 40.0) / NULLIF(SUM(ISNULL(e.MaxGrade,0)),0)
    FROM Results r
    INNER JOIN Exams e ON r.ExamID = e.ExamID
    WHERE r.StudentID = @StudentID
      AND e.SubjectID = @SubjectID
      AND e.ExamPeriodID = @ExamPeriodID;

    IF @ExamPoints IS NULL SET @ExamPoints = 0;

    -- «·„Ã„Ê⁄ «·‰Â«∆Ì
    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    -- ≈œŒ«· √Ê  ÕœÌÀ «·”Ã· ›Ì ÃœÊ· FinalGrades
    IF EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND SubjectID = @SubjectID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          AND ExamTypeID = @ExamTypeID
    )
    BEGIN
        UPDATE FinalGrades
        SET Grade = @FinalGrade,
            MaxGrade = 100,
            CalculationDate = GETDATE()
        WHERE StudentID = @StudentID
          AND SubjectID = @SubjectID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          AND ExamTypeID = @ExamTypeID;
    END
    ELSE
    BEGIN
        INSERT INTO FinalGrades
        (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade, MaxGrade, CalculationDate)
        VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, @ExamTypeID, @FinalGrade, 100, GETDATE());
    END

END;

go

alter TRIGGER trg_AfterInsertUpdate_Attendance
ON StudentAttendance
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- „À«· ⁄·Ï ”Ã· Ê«Õœ ›ﬁÿ „‰ inserted (Ì„ﬂ‰  Ê”Ì⁄ ·œ⁄„ √ﬂÀ—)
    DECLARE @StudentID INT;
    SELECT TOP 1 @StudentID = i.StudentID FROM inserted i;

    -- ‰Õ «Ã  ÕœÌœ › —… «·«„ Õ«‰ «· Ì  Õ ÊÌ  «—ÌŒ «·Õ÷Ê—
    DECLARE @AttendanceDate DATE;
    SELECT TOP 1 @AttendanceDate = AttendanceDate FROM inserted;

    DECLARE @ExamPeriodID INT;
    SELECT TOP 1 @ExamPeriodID = ExamPeriodID 
    FROM ExamPeriods 
    WHERE @AttendanceDate BETWEEN StartDate AND EndDate;

    IF @ExamPeriodID IS NULL RETURN; -- ·«  ÊÃœ › —… „‰«”»…

    -- Ã·» »Ì«‰«  «·ÿ«·» „‰ StudentStages ·—»ÿ »«·„—Õ·… Ê«·”‰…
    DECLARE @StageID INT, @AcademicYearID INT;
    SELECT TOP 1 @StageID = ss.StageID, @AcademicYearID = ss.AcademicYearID 
    FROM StudentStages ss 
    WHERE ss.StudentID = @StudentID AND ss.IsActive = 1;

    -- ‰Õ «Ã Õ”«» ‰›” ‰ﬁ«ÿ «·Õ÷Ê— Ê«·«‰÷»«ÿ Ê«·«Œ »«— ﬂ„« ›Ì Trigger «·√Ê·

    -- Õ”«» ‰ﬁ«ÿ «·Õ÷Ê— (20%)
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    SELECT @AttendancePoints = 
        (COUNT(CASE WHEN Status = 'Present' THEN 1 END) * 20.0) /
        NULLIF(COUNT(*),0)
    FROM StudentAttendance
    WHERE StudentID = @StudentID
      AND AttendanceDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                             AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @AttendancePoints IS NULL SET @AttendancePoints = 0;

    -- ‰ﬁ«ÿ «·«‰÷»«ÿ (”Ì „ Õ”«» ›Ì Trigger Œ«’ »«·«‰÷»«ÿ° ·ﬂ‰ ‰÷⁄ ’›— „ƒﬁ «)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;

    -- ‰ﬁ«ÿ «·«„ Õ«‰ (40%) ñ ·« Ì„ﬂ‰ Õ”«» Â‰« ·√‰Â Ì⁄ „œ ⁄·Ï ÃœÊ· Results° ‰÷⁄ ’›— „ƒﬁ «
    DECLARE @ExamPoints DECIMAL(5,2) = 0;

    -- «·„Ã„Ê⁄ «·‰Â«∆Ì Â‰« ÂÊ „Ã—œ  ﬁœÌ—° ·ﬂ‰ ·« „‘ﬂ·… ·ÊÃÊœ ‰«ﬁ’
    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    -- · ÕœÌÀ FinalGrades ‰Õ «Ã „⁄—›… «·„«œ… Ê‰Ê⁄ «·«„ Õ«‰° Â‰« ‰› —÷  ÕœÌÀ ·Ã„Ì⁄ «·„Ê«œ Ê‰Ê⁄ «„ Õ«‰ („À·« ‰÷⁄ ExamTypeID = 1 «› —«÷«)
    -- ›Ì «· ÿ»Ìﬁ «·⁄„·Ì  Õ «Ã  ⁄œÌ·«  ·—»ÿ «·„«œ… Ê‰Ê⁄ «·«„ Õ«‰ »‘ﬂ· ’ÕÌÕ

    IF EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          -- AND SubjectID = ? -- ‰Õ «Ã ≈÷«› Â« Õ”» «· ÿ»Ìﬁ
          -- AND ExamTypeID = ? -- ‰Õ «Ã ≈÷«› Â« Õ”» «· ÿ»Ìﬁ
    )
    BEGIN
        UPDATE FinalGrades
        SET Grade = @FinalGrade,
            CalculationDate = GETDATE()
        WHERE StudentID = @StudentID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID;
    END
    ELSE
    BEGIN
        INSERT INTO FinalGrades
        (StudentID, StageID, AcademicYearID, ExamPeriodID, Grade, MaxGrade, CalculationDate)
        VALUES (@StudentID, @StageID, @AcademicYearID, @ExamPeriodID, @FinalGrade, 100, GETDATE());
    END
END;



go


CREATE TRIGGER trg_AfterInsertUpdate_Discipline
ON StudentDiscipline
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @DisciplineDate DATE;
    SELECT TOP 1 @StudentID = i.StudentID, @DisciplineDate = i.DisciplineDate FROM inserted i;

    DECLARE @ExamPeriodID INT;
    SELECT TOP 1 @ExamPeriodID = ExamPeriodID 
    FROM ExamPeriods
    WHERE @DisciplineDate BETWEEN StartDate AND EndDate;

    IF @ExamPeriodID IS NULL RETURN;

    DECLARE @StageID INT, @AcademicYearID INT;
    SELECT TOP 1 @StageID = ss.StageID, @AcademicYearID = ss.AcademicYearID
    FROM StudentStages ss
    WHERE ss.StudentID = @StudentID AND ss.IsActive = 1;

    -- Õ”«» ‰ﬁ«ÿ «·«‰÷»«ÿ (40%)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;
    SELECT @DisciplinePoints = AVG(CAST(Points AS DECIMAL(5,2))) * 0.4
    FROM Discipline
    WHERE StudentID = @StudentID
      AND DisciplineDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                              AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @DisciplinePoints IS NULL SET @DisciplinePoints = 0;

    -- ‰÷⁄ ’›— ··Õ÷Ê— Ê«·«Œ »«— „ƒﬁ «° ·√‰ Â–« Trigger Œ«’ »«·«‰÷»«ÿ ›ﬁÿ
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    DECLARE @ExamPoints DECIMAL(5,2) = 0;

    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    --  ÕœÌÀ √Ê ≈œŒ«· ›Ì ÃœÊ· FinalGrades (»–«  «·„·«ÕŸ«  ÕÊ· «·„«œ… Ê‰Ê⁄ «·«„ Õ«‰ ﬂ„« ›Ì Trigger «·”«»ﬁ)
    IF EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
    )
    BEGIN
        UPDATE FinalGrades
        SET Grade = @FinalGrade,
            CalculationDate = GETDATE()
        WHERE StudentID = @StudentID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID;
    END
    ELSE
    BEGIN
        INSERT INTO FinalGrades
        (StudentID, StageID, AcademicYearID, ExamPeriodID, Grade, MaxGrade, CalculationDate)
        VALUES (@StudentID, @StageID, @AcademicYearID, @ExamPeriodID, @FinalGrade, 100, GETDATE());
    END
END;








