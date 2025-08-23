CREATE PROCEDURE sp_CalculateWeeklyGrade
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT,
    @ExamPeriodID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;
    DECLARE @ExamPoints DECIMAL(5,2) = 0;
    DECLARE @FinalGrade DECIMAL(5,2) = 0;

    -- ÍÓÇÈ ÇáÍÖæÑ (20%)
    SELECT @AttendancePoints = 
        (COUNT(CASE WHEN Status = 'Present' THEN 1 END) * 1.0 / NULLIF(COUNT(*),0)) * 20
    FROM StudentAttendance SA
    JOIN ExamPeriods EP ON EP.ExamPeriodID = @ExamPeriodID
    WHERE SA.StudentID = @StudentID 
      AND AttendanceDate BETWEEN EP.StartDate AND EP.EndDate;

    -- ÍÓÇÈ ÇáÇäÖÈÇØ (40%)
    SELECT @DisciplinePoints = 
        ISNULL(AVG(Points), 100) * 0.4
    FROM Discipline
    WHERE StudentID = @StudentID 
      AND DisciplineDate BETWEEN 
        (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
        AND
        (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    -- ÍÓÇÈ ÏÑÌÉ ÇáÇÎÊÈÇÑ ÇáæÑÞí (40%)
    SELECT TOP 1 @ExamPoints = 
        ISNULL((R.Score / NULLIF(E.MaxGrade,0)) * 40, 0)
    FROM Results R
    JOIN Exams E ON E.ExamID = R.ExamID
    WHERE R.StudentID = @StudentID
      AND E.SubjectID = @SubjectID
      AND E.ExamPeriodID = @ExamPeriodID;

    SET @FinalGrade = ROUND(ISNULL(@AttendancePoints,0) + ISNULL(@DisciplinePoints,0) + ISNULL(@ExamPoints,0), 2);

    -- ãäÚ ÇáÊßÑÇÑ ÞÈá ÇáÅÏÎÇá
    IF NOT EXISTS (
        SELECT 1 FROM FinalGrades 
        WHERE StudentID = @StudentID 
          AND SubjectID = @SubjectID 
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID 
          AND ExamTypeID = 1
    )
    BEGIN
        INSERT INTO FinalGrades (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade)
        VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, 1, @FinalGrade);
    END
END


go




CREATE PROCEDURE sp_CalculateMonthlyGrade
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT,
    @ExamPeriodID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @WeeklyAvg DECIMAL(5,2) = 0;
    DECLARE @MonthlyExamPoints DECIMAL(5,2) = 0;
    DECLARE @FinalGrade DECIMAL(5,2) = 0;

    SELECT @WeeklyAvg = ISNULL(AVG(Grade), 0)
    FROM FinalGrades FG
    JOIN ExamPeriods EP ON EP.ExamPeriodID = FG.ExamPeriodID
    WHERE FG.StudentID = @StudentID
      AND FG.SubjectID = @SubjectID
      AND FG.StageID = @StageID
      AND FG.AcademicYearID = @AcademicYearID
      AND FG.ExamTypeID = 1
      AND EP.StartDate >= (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
      AND EP.EndDate <= (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    SELECT TOP 1 @MonthlyExamPoints = 
        ISNULL((R.Score / NULLIF(E.MaxGrade,0)) * 100, 0)
    FROM Results R
    JOIN Exams E ON E.ExamID = R.ExamID
    WHERE R.StudentID = @StudentID
      AND E.SubjectID = @SubjectID
      AND E.ExamPeriodID = @ExamPeriodID;

    SET @FinalGrade = ROUND((@WeeklyAvg * 0.6) + (@MonthlyExamPoints * 0.4), 2);

    IF NOT EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND SubjectID = @SubjectID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          AND ExamTypeID = 2
    )
    BEGIN
        INSERT INTO FinalGrades (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade)
        VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, 2, @FinalGrade);
    END
END




go







CREATE PROCEDURE sp_CalculateTermGrade
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT,
    @ExamPeriodID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MonthlyAvg DECIMAL(5,2) = 0;
    DECLARE @TermExamPoints DECIMAL(5,2) = 0;
    DECLARE @FinalGrade DECIMAL(5,2) = 0;

    SELECT @MonthlyAvg = ISNULL(AVG(Grade), 0)
    FROM FinalGrades FG
    JOIN ExamPeriods EP ON EP.ExamPeriodID = FG.ExamPeriodID
    WHERE FG.StudentID = @StudentID
      AND FG.SubjectID = @SubjectID
      AND FG.StageID = @StageID
      AND FG.AcademicYearID = @AcademicYearID
      AND FG.ExamTypeID = 2
      AND EP.StartDate >= (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
      AND EP.EndDate <= (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    SELECT TOP 1 @TermExamPoints = 
        ISNULL((R.Score / NULLIF(E.MaxGrade,0)) * 100, 0)
    FROM Results R
    JOIN Exams E ON E.ExamID = R.ExamID
    WHERE R.StudentID = @StudentID
      AND E.SubjectID = @SubjectID
      AND E.ExamPeriodID = @ExamPeriodID;

    SET @FinalGrade = ROUND((@MonthlyAvg * 0.6) + (@TermExamPoints * 0.4), 2);

    IF NOT EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND SubjectID = @SubjectID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          AND ExamTypeID = 3
    )
    BEGIN
        INSERT INTO FinalGrades (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade)
        VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, 3, @FinalGrade);
    END
END

go







CREATE PROCEDURE sp_CalculateYearlyGrade
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT,
    @ExamPeriodID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @TermAvg DECIMAL(5,2) = 0;

    SELECT @TermAvg = ISNULL(AVG(Grade), 0)
    FROM FinalGrades FG
    JOIN ExamPeriods EP ON EP.ExamPeriodID = FG.ExamPeriodID
    WHERE FG.StudentID = @StudentID
      AND FG.SubjectID = @SubjectID
      AND FG.StageID = @StageID
      AND FG.AcademicYearID = @AcademicYearID
      AND FG.ExamTypeID = 3
      AND EP.StartDate >= (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
      AND EP.EndDate <= (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF NOT EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND SubjectID = @SubjectID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          AND ExamTypeID = 4
    )
    BEGIN
        INSERT INTO FinalGrades (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade)
        VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, 4, @TermAvg);
    END
END

go


CREATE PROCEDURE sp_CalculateAllGradesForStudent
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ExamPeriodID INT;

    -- ÇáÃÓÇÈíÚ (1)
    DECLARE WeeklyCursor CURSOR FOR
    SELECT ExamPeriodID FROM ExamPeriods WHERE ExamTypeID = 1 AND AcademicYearID = @AcademicYearID;
    OPEN WeeklyCursor;
    FETCH NEXT FROM WeeklyCursor INTO @ExamPeriodID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC sp_CalculateWeeklyGrade @StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID;
        FETCH NEXT FROM WeeklyCursor INTO @ExamPeriodID;
    END
    CLOSE WeeklyCursor;
    DEALLOCATE WeeklyCursor;

    -- ÇáÔåæÑ (2)
    DECLARE MonthlyCursor CURSOR FOR
    SELECT ExamPeriodID FROM ExamPeriods WHERE ExamTypeID = 2 AND AcademicYearID = @AcademicYearID;
    OPEN MonthlyCursor;
    FETCH NEXT FROM MonthlyCursor INTO @ExamPeriodID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC sp_CalculateMonthlyGrade @StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID;
        FETCH NEXT FROM MonthlyCursor INTO @ExamPeriodID;
    END
    CLOSE MonthlyCursor;
    DEALLOCATE MonthlyCursor;

    -- ÇáÝÕæá (3)
    DECLARE TermCursor CURSOR FOR
    SELECT ExamPeriodID FROM ExamPeriods WHERE ExamTypeID = 3 AND AcademicYearID = @AcademicYearID;
    OPEN TermCursor;
    FETCH NEXT FROM TermCursor INTO @ExamPeriodID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC sp_CalculateTermGrade @StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID;
        FETCH NEXT FROM TermCursor INTO @ExamPeriodID;
    END
    CLOSE TermCursor;
    DEALLOCATE TermCursor;

    -- ÇáÓäÉ (4)
    DECLARE YearCursor CURSOR FOR
    SELECT ExamPeriodID FROM ExamPeriods WHERE ExamTypeID = 4 AND AcademicYearID = @AcademicYearID;
    OPEN YearCursor;
    FETCH NEXT FROM YearCursor INTO @ExamPeriodID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        EXEC sp_CalculateYearlyGrade @StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID;
        FETCH NEXT FROM YearCursor INTO @ExamPeriodID;
    END
    CLOSE YearCursor;
    DEALLOCATE YearCursor;

END



go

CREATE PROCEDURE sp_CalculateAllGrades_AllStudents_AllSubjects
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT;
    DECLARE @SubjectID INT;
    DECLARE @StageID INT;
    DECLARE @AcademicYearID INT;

    SELECT TOP 1 @AcademicYearID = AcademicYearID
    FROM AcademicYears
    WHERE GETDATE() BETWEEN StartDate AND EndDate;

    DECLARE student_cursor CURSOR FOR
    SELECT StudentID, StageID FROM Students WHERE IsActive = 1;

    OPEN student_cursor;
    FETCH NEXT FROM student_cursor INTO @StudentID, @StageID;
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE subject_cursor CURSOR FOR
        SELECT SubjectID FROM Subjects WHERE IsActive = 1 AND StageID = @StageID;

        OPEN subject_cursor;
        FETCH NEXT FROM subject_cursor INTO @SubjectID;
        WHILE @@FETCH_STATUS = 0
        BEGIN
            EXEC sp_CalculateAllGradesForStudent
                @StudentID = @StudentID,
                @SubjectID = @SubjectID,
                @StageID = @StageID,
                @AcademicYearID = @AcademicYearID;

            FETCH NEXT FROM subject_cursor INTO @SubjectID;
        END
        CLOSE subject_cursor;
        DEALLOCATE subject_cursor;

        FETCH NEXT FROM student_cursor INTO @StudentID, @StageID;
    END
    CLOSE student_cursor;
    DEALLOCATE student_cursor;
END

go









