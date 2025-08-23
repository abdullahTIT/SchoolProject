CREATE TABLE FinalGrades (
    FinalGradeID INT PRIMARY KEY IDENTITY(1,1),
    StudentID INT NOT NULL,
    SubjectID INT NOT NULL,
    StageID INT NOT NULL,
    AcademicYearID INT NOT NULL,
    ExamPeriodID INT NOT NULL, -- «·√”»Ê⁄° «·‘Â—° «· —„° «·”‰…
    Grade DECIMAL(5,2) NOT NULL CHECK (Grade BETWEEN 0 AND 100),
    CreatedDate DATETIME DEFAULT GETDATE(),

    CONSTRAINT FK_FinalGrades_Student FOREIGN KEY (StudentID) REFERENCES Students(StudentID),
    CONSTRAINT FK_FinalGrades_Subject FOREIGN KEY (SubjectID) REFERENCES Subjects(SubjectID),
    CONSTRAINT FK_FinalGrades_Stage FOREIGN KEY (StageID) REFERENCES Stages(StageID),
    CONSTRAINT FK_FinalGrades_AcademicYear FOREIGN KEY (AcademicYearID) REFERENCES AcademicYears(AcademicYearID),
    CONSTRAINT FK_FinalGrades_ExamPeriod FOREIGN KEY (ExamPeriodID) REFERENCES ExamPeriods(ExamPeriodID)
);
go

drop TRIGGER trg_CalculateWeeklyGrade
alter TRIGGER trg_CalculateWeeklyGrade
ON Results
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @ExamID INT, @ExamPeriodID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT;
    DECLARE @Score DECIMAL(5,2), @DisciplinePoints INT;

    SELECT TOP 1
        @StudentID = i.StudentID,
        @ExamID = i.ExamID,
        @Score = i.Score
    FROM inserted i;
    SELECT
        @ExamPeriodID = e.ExamPeriodID,
        @SubjectID = e.SubjectID,
        @StageID = s.StageID,
        @AcademicYearID = YEAR(e.ExamDate)
    FROM Exams e
    JOIN Students s ON s.StudentID = @StudentID
    WHERE e.ExamID = @ExamID;

    -- ‰ Õﬁﬁ Â· ÂÊ «”»Ê⁄
    IF EXISTS (SELECT 1 FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID AND ExamTypeID = 1) -- 1 = Week
    BEGIN
        -- «Ã·» ‰ﬁ«ÿ «·«‰÷»«ÿ ·–·ﬂ «·ÿ«·» ›Ì ‰›” «·√”»Ê⁄
        SELECT @DisciplinePoints = ISNULL(SUM(Points), 0)
        FROM StudentDiscipline
        WHERE StudentID = @StudentID
        AND DisciplineDate BETWEEN
            (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
            AND
            (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

        DECLARE @FinalGrade DECIMAL(5,2) = (@Score + ISNULL(@DisciplinePoints, 0)) / 2;

        --  ÕœÌÀ √Ê ≈œ—«Ã ›Ì FinalGrades
        IF EXISTS (
            SELECT 1 FROM FinalGrades
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @ExamPeriodID
        )
        BEGIN
            UPDATE FinalGrades
            SET Grade = @FinalGrade, CreatedDate = GETDATE()
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @ExamPeriodID;
        END
        ELSE
        BEGIN
            INSERT INTO FinalGrades (StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
            VALUES (@StudentID, @SubjectID, @StageID, @AcademicYearID, @ExamPeriodID, @FinalGrade);
        END
    END
END;

go
drop PROCEDURE sp_CalculateAllFinalGrades
ALTER PROCEDURE sp_CalculateAllFinalGrades
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT, @ExamPeriodID INT;

    --  ÕœÌœ «·”‰… «·œ—«”Ì… «·Õ«·Ì…
    SELECT TOP 1 @AcademicYearID = AcademicYearID
    FROM AcademicYears
    WHERE GETDATE() BETWEEN StartDate AND EndDate;

    --  ‰ŸÌ› «·ﬂÌ—”— ≈–« ﬂ«‰ „ÊÃÊœ« „‰ ﬁ»·
    IF CURSOR_STATUS('global', 'cur') >= -1
    BEGIN
        CLOSE cur;
        DEALLOCATE cur;
    END

    -- ·ﬂ· «·ÿ·«» Ê«·„Ê«œ
    DECLARE cur CURSOR FOR
    SELECT DISTINCT s.StudentID, e.SubjectID, s.StageID
    FROM Students s
    JOIN Exams e ON e.ExamDate BETWEEN 
        (SELECT StartDate FROM AcademicYears WHERE AcademicYearID = @AcademicYearID)
        AND 
        (SELECT EndDate FROM AcademicYears WHERE AcademicYearID = @AcademicYearID);

    OPEN cur;
    FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Õ”«» œ—Ã… «·‘Â—
        DECLARE @MonthExamPeriodID INT;

        SELECT @MonthExamPeriodID = ep.ExamPeriodID
        FROM ExamPeriods ep
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Month' AND ep.AcademicYearID = @AcademicYearID;

        DECLARE @WeeksAverage DECIMAL(5,2) = (
            SELECT AVG(Grade)
            FROM FinalGrades fg
            JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
            JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
            WHERE et.ExamTypeName = 'Week'
              AND fg.StudentID = @StudentID 
              AND fg.SubjectID = @SubjectID 
              AND fg.AcademicYearID = @AcademicYearID
        );

        DECLARE @AbsentsCount INT = (
            SELECT COUNT(*) 
            FROM StudentAttendance sa
            JOIN Schedules sc ON sa.ScheduleID = sc.ScheduleID
            WHERE sa.Status = 'Absent'
              AND sa.StudentID = @StudentID
              AND sc.SubjectID = @SubjectID
              AND sc.AcademicYearID = @AcademicYearID
        );

        DECLARE @AttendancePoints DECIMAL(5,2) = CASE
            WHEN @AbsentsCount IS NULL THEN 20
            ELSE 20 - @AbsentsCount
        END;

        DECLARE @MonthTestScore DECIMAL(5,2) = (
            SELECT TOP 1 r.Score
            FROM Results r
            JOIN Exams e ON r.ExamID = e.ExamID
            WHERE r.StudentID = @StudentID
              AND e.SubjectID = @SubjectID
              AND e.ExamPeriodID = @MonthExamPeriodID
        );

        DECLARE @MonthGrade DECIMAL(5,2) = 
            ISNULL(@WeeksAverage,0)*0.2 + ISNULL(@AttendancePoints,0) + ISNULL(@MonthTestScore,0)*0.6;

        -- ≈œ—«Ã √Ê  ÕœÌÀ
        IF EXISTS (
            SELECT 1 FROM FinalGrades
            WHERE StudentID = @StudentID 
              AND SubjectID = @SubjectID 
              AND ExamPeriodID = @MonthExamPeriodID
        )
        BEGIN
            UPDATE FinalGrades
            SET Grade = @MonthGrade
            WHERE StudentID = @StudentID 
              AND SubjectID = @SubjectID 
              AND ExamPeriodID = @MonthExamPeriodID;
        END
        ELSE
        BEGIN
            INSERT INTO FinalGrades(StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
            VALUES(@StudentID, @SubjectID, @StageID, @AcademicYearID, @MonthExamPeriodID, @MonthGrade);
        END

        FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;
GO
go

CREATE PROCEDURE sp_CalculateTermGrades
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT;

    SELECT TOP 1 @AcademicYearID = AcademicYearID
    FROM AcademicYears
    WHERE GETDATE() BETWEEN StartDate AND EndDate;

DECLARE cur CURSOR FOR
SELECT DISTINCT s.StudentID, e.SubjectID, s.StageID
FROM Students s
JOIN Exams e ON e.ExamDate BETWEEN 
    (SELECT StartDate FROM AcademicYears WHERE AcademicYearID = @AcademicYearID)
    AND 
    (SELECT EndDate FROM AcademicYears WHERE AcademicYearID = @AcademicYearID);

    OPEN cur;
    FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @MonthAverage DECIMAL(5,2);
        SELECT @MonthAverage = AVG(Grade)
        FROM FinalGrades fg
        JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Month'
          AND fg.StudentID = @StudentID
          AND fg.SubjectID = @SubjectID
          AND fg.AcademicYearID = @AcademicYearID;

        DECLARE @TermExamPeriodID INT;
        SELECT @TermExamPeriodID = ep.ExamPeriodID
        FROM ExamPeriods ep
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Term'
          AND ep.AcademicYearID = @AcademicYearID;

        DECLARE @TermTestScore DECIMAL(5,2);
        SELECT TOP 1 @TermTestScore = r.Score
        FROM Results r
        JOIN Exams e ON r.ExamID = e.ExamID
        WHERE r.StudentID = @StudentID
          AND e.SubjectID = @SubjectID
          AND e.ExamPeriodID = @TermExamPeriodID;

        DECLARE @TermGrade DECIMAL(5,2) = ISNULL(@MonthAverage,0)*0.4 + ISNULL(@TermTestScore,0)*0.6;

        IF EXISTS (
            SELECT 1 FROM FinalGrades
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @TermExamPeriodID
        )
            UPDATE FinalGrades
            SET Grade = @TermGrade
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @TermExamPeriodID;
        ELSE
            INSERT INTO FinalGrades(StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
            VALUES(@StudentID, @SubjectID, @StageID, @AcademicYearID, @TermExamPeriodID, @TermGrade);

        FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;

go 


CREATE PROCEDURE sp_CalculateYearGrades
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT;

    SELECT TOP 1 @AcademicYearID = AcademicYearID
    FROM AcademicYears
    WHERE GETDATE() BETWEEN StartDate AND EndDate;

    DECLARE cur CURSOR FOR
    SELECT DISTINCT s.StudentID, e.SubjectID, s.StageID
    FROM Students s
    JOIN Exams e ON YEAR(e.ExamDate) = @AcademicYearID;

    OPEN cur;
    FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @MonthAverage DECIMAL(5,2);
        SELECT @MonthAverage = AVG(Grade)
        FROM FinalGrades fg
        JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Month'
          AND fg.StudentID = @StudentID
          AND fg.SubjectID = @SubjectID
          AND fg.AcademicYearID = @AcademicYearID;

        DECLARE @YearExamPeriodID INT;
        SELECT @YearExamPeriodID = ep.ExamPeriodID
        FROM ExamPeriods ep
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Year'
          AND ep.AcademicYearID = @AcademicYearID;

        DECLARE @YearGrade DECIMAL(5,2) = ISNULL(@MonthAverage, 0);

        IF EXISTS (
            SELECT 1 FROM FinalGrades
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @YearExamPeriodID
        )
            UPDATE FinalGrades
            SET Grade = @YearGrade
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @YearExamPeriodID;
        ELSE
            INSERT INTO FinalGrades(StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
            VALUES(@StudentID, @SubjectID, @StageID, @AcademicYearID, @YearExamPeriodID, @YearGrade);

        FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;
go

select * from vw_StudentFinalGrades

CREATE VIEW vw_StudentFinalGrades AS
SELECT 
    s.StudentID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    st.StageName,
    sub.SubjectName,
    et.ExamTypeName,
    fg.Grade
FROM FinalGrades fg
JOIN Students s ON fg.StudentID = s.StudentID
JOIN Persons p ON s.PersonID = p.PersonID
JOIN Stages st ON fg.StageID = st.StageID
JOIN Subjects sub ON fg.SubjectID = sub.SubjectID
JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
go

EXEC sp_CalculateAllFinalGrades;
EXEC sp_CalculateTermGrades;
EXEC sp_CalculateYearGrades;


SELECT * FROM vw_StudentFinalGrades
WHERE ExamTypeName = 'Year'; -- √Ê 'Month' √Ê 'Term' √Ê 'Week'
go


CREATE VIEW vw_StudentAverageGrades AS
SELECT 
    s.StudentID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    st.StageName,
    ay.AcademicYearName,
    et.ExamTypeName,
    AVG(fg.Grade) AS AverageGrade,
    COUNT(DISTINCT fg.SubjectID) AS SubjectsCount
FROM FinalGrades fg
JOIN Students s ON fg.StudentID = s.StudentID
JOIN Persons p ON s.PersonID = p.PersonID
JOIN Stages st ON fg.StageID = st.StageID
JOIN AcademicYears ay ON fg.AcademicYearID = ay.AcademicYearID
JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
GROUP BY 
    s.StudentID,
    p.FirstName, p.SecondName, p.ThirdName, p.LastName,
    st.StageName,
    ay.AcademicYearName,
    et.ExamTypeName

go
	CREATE TRIGGER trg_AutoCalcGrades_AfterResult
ON Results
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    EXEC sp_CalculateAllFinalGrades;
END;
GO



	SELECT * FROM vw_StudentAverageGrades
WHERE ExamTypeName = 'Year';


SELECT * FROM vw_StudentAverageGrades
WHERE ExamTypeName = 'Month';

go

CREATE VIEW vw_TopStudentPerStageYear AS
WITH RankedGrades AS (
    SELECT 
        s.StudentID,
        p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
        st.StageName,
        ay.AcademicYearName,
        AVG(fg.Grade) AS AverageGrade,
        RANK() OVER (PARTITION BY st.StageID, fg.AcademicYearID ORDER BY AVG(fg.Grade) DESC) AS RankInStage
    FROM FinalGrades fg
    JOIN Students s ON fg.StudentID = s.StudentID
    JOIN Persons p ON s.PersonID = p.PersonID
    JOIN Stages st ON fg.StageID = st.StageID
    JOIN AcademicYears ay ON fg.AcademicYearID = ay.AcademicYearID
    JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
    JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
    WHERE et.ExamTypeName = 'Year'
    GROUP BY s.StudentID, p.FirstName, p.SecondName, p.ThirdName, p.LastName, st.StageName, st.StageID, fg.AcademicYearID, ay.AcademicYearName
)
SELECT *
FROM RankedGrades
WHERE RankInStage = 1;




go

CREATE VIEW vw_StudentPassFailStatus AS
SELECT 
    s.StudentID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    st.StageName,
    ay.AcademicYearName,
    AVG(fg.Grade) AS AverageGrade,
    CASE 
        WHEN AVG(fg.Grade) >= 50 THEN 'pass'
        ELSE 'fail'
    END AS FinalStatus
FROM FinalGrades fg
JOIN Students s ON fg.StudentID = s.StudentID
JOIN Persons p ON s.PersonID = p.PersonID
JOIN Stages st ON fg.StageID = st.StageID
JOIN AcademicYears ay ON fg.AcademicYearID = ay.AcademicYearID
JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
WHERE et.ExamTypeName = 'Year'
GROUP BY s.StudentID, p.FirstName, p.SecondName, p.ThirdName, p.LastName, st.StageName, ay.AcademicYearName;


go
SELECT * FROM vw_TopStudentPerStageYear;


SELECT * FROM vw_StudentPassFailStatus
WHERE FinalStatus = 'pass';




go


alter PROCEDURE sp_CalculateMonthGrades
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT;

    -- «·Õ’Ê· ⁄·Ï «·”‰… «·√ﬂ«œÌ„Ì… «·Õ«·Ì…
    SELECT TOP 1 @AcademicYearID = AcademicYearID
    FROM AcademicYears
    WHERE GETDATE() BETWEEN StartDate AND EndDate;

    -- ﬂ· «·ÿ·«» Ê«·„Ê«œ
    DECLARE cur CURSOR FOR
    SELECT DISTINCT s.StudentID, sc.SubjectID, s.StageID
    FROM Students s
    JOIN Schedules sc ON sc.AcademicYearID = @AcademicYearID;

    OPEN cur;
    FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;

    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @MonthExamPeriodID INT;

        SELECT TOP 1 @MonthExamPeriodID = ep.ExamPeriodID
        FROM ExamPeriods ep
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Month' AND ep.AcademicYearID = @AcademicYearID;

        -- 1. „ Ê”ÿ œ—Ã«  «·√”«»Ì⁄
        DECLARE @WeeksAvg DECIMAL(5,2) = (
            SELECT AVG(Grade)
            FROM FinalGrades fg
            JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
            JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
            WHERE et.ExamTypeName = 'Week'
              AND fg.StudentID = @StudentID
              AND fg.SubjectID = @SubjectID
              AND fg.AcademicYearID = @AcademicYearID
        );

        -- 2. Õ”«» «·Õ÷Ê—: ﬂ· €Ì«» = -1 ‰ﬁÿ… „‰ 20
        DECLARE @AbsentsCount INT = (
            SELECT COUNT(*)
            FROM StudentAttendance sa
            JOIN Schedules sc ON sa.ScheduleID = sc.ScheduleID
            WHERE sa.Status = 'Absent'
              AND sa.StudentID = @StudentID
              AND sc.SubjectID = @SubjectID
              AND sc.AcademicYearID = @AcademicYearID
        );

        DECLARE @AttendancePoints DECIMAL(5,2) = 20 - ISNULL(@AbsentsCount, 0);

        -- 3. «Œ »«— «·‘Â—
        DECLARE @MonthScore DECIMAL(5,2) = (
            SELECT TOP 1 r.Score
            FROM Results r
            JOIN Exams e ON r.ExamID = e.ExamID
            WHERE r.StudentID = @StudentID
              AND e.SubjectID = @SubjectID
              AND e.ExamPeriodID = @MonthExamPeriodID
        );

        -- 4. Õ”«» «·œ—Ã… «·‰Â«∆Ì… ··‘Â—
        DECLARE @MonthGrade DECIMAL(5,2) =
            ISNULL(@WeeksAvg, 0) * 0.2 +
            ISNULL(@AttendancePoints, 0) +
            ISNULL(@MonthScore, 0) * 0.6;

        -- ≈œ—«Ã √Ê  ÕœÌÀ «·‰ ÌÃ…
        IF EXISTS (
            SELECT 1 FROM FinalGrades
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @MonthExamPeriodID
        )
        BEGIN
            UPDATE FinalGrades
            SET Grade = @MonthGrade, CreatedDate = GETDATE()
            WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @MonthExamPeriodID;
        END
        ELSE
        BEGIN
            INSERT INTO FinalGrades(StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
            VALUES(@StudentID, @SubjectID, @StageID, @AcademicYearID, @MonthExamPeriodID, @MonthGrade);
        END

        FETCH NEXT FROM cur INTO @StudentID, @SubjectID, @StageID;
    END

    CLOSE cur;
    DEALLOCATE cur;
END;

go

EXEC sp_CalculateMonthGrades;
go

alter PROCEDURE sp_CalculateMonthGrades_ForStudentSubject
    @StudentID INT,
    @SubjectID INT,
    @StageID INT,
    @AcademicYearID INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MonthExamPeriodID INT;

    SELECT TOP 1 @MonthExamPeriodID = ep.ExamPeriodID
    FROM ExamPeriods ep
    JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
    WHERE et.ExamTypeName = 'Month' AND ep.AcademicYearID = @AcademicYearID;

    -- 1. „ Ê”ÿ œ—Ã«  «·√”«»Ì⁄
    DECLARE @WeeksAvg DECIMAL(5,2) = (
        SELECT AVG(Grade)
        FROM FinalGrades fg
        JOIN ExamPeriods ep ON fg.ExamPeriodID = ep.ExamPeriodID
        JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
        WHERE et.ExamTypeName = 'Week'
          AND fg.StudentID = @StudentID
          AND fg.SubjectID = @SubjectID
          AND fg.AcademicYearID = @AcademicYearID
    );

    -- 2. Õ”«» «·Õ÷Ê—: ﬂ· €Ì«» = -1 ‰ﬁÿ… „‰ 20
    DECLARE @AbsentsCount INT = (
        SELECT COUNT(*)
        FROM StudentAttendance sa
        JOIN Schedules sc ON sa.ScheduleID = sc.ScheduleID
        WHERE sa.Status = 'Absent'
          AND sa.StudentID = @StudentID
          AND sc.SubjectID = @SubjectID
          AND sc.AcademicYearID = @AcademicYearID
    );

    DECLARE @AttendancePoints DECIMAL(5,2) = 20 - ISNULL(@AbsentsCount, 0);

    -- 3. «Œ »«— «·‘Â—
    DECLARE @MonthScore DECIMAL(5,2) = (
        SELECT TOP 1 r.Score
        FROM Results r
        JOIN Exams e ON r.ExamID = e.ExamID
        WHERE r.StudentID = @StudentID
          AND e.SubjectID = @SubjectID
          AND e.ExamPeriodID = @MonthExamPeriodID
    );

    -- 4. Õ”«» «·œ—Ã… «·‰Â«∆Ì… ··‘Â—
    DECLARE @MonthGrade DECIMAL(5,2) =
        ISNULL(@WeeksAvg, 0) * 0.2 +
        ISNULL(@AttendancePoints, 0) +
        ISNULL(@MonthScore, 0) * 0.6;

    -- ≈œ—«Ã √Ê  ÕœÌÀ «·‰ ÌÃ…
    IF EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @MonthExamPeriodID
    )
    BEGIN
        UPDATE FinalGrades
        SET Grade = @MonthGrade, CreatedDate = GETDATE()
        WHERE StudentID = @StudentID AND SubjectID = @SubjectID AND ExamPeriodID = @MonthExamPeriodID;
    END
    ELSE
    BEGIN
        INSERT INTO FinalGrades(StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, Grade)
        VALUES(@StudentID, @SubjectID, @StageID, @AcademicYearID, @MonthExamPeriodID, @MonthGrade);
    END
END;
go

alter TRIGGER trg_RunMonthGradesCalculation_Specific
ON Results
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @StudentID INT, @SubjectID INT, @StageID INT, @AcademicYearID INT;

    SELECT TOP 1 
        i.StudentID,
        e.SubjectID,
        s.StageID,
        s2.AcademicYearID
    INTO #temp
    FROM inserted i
    JOIN Exams e ON i.ExamID = e.ExamID
    JOIN Students s ON i.StudentID = s.StudentID
    JOIN StudentStages s2 ON s.StudentID = s2.StudentID AND s2.IsActive = 1 -- «·”‰… Ê«·„—Õ·… «·Õ«·Ì…
    JOIN ExamPeriods ep ON e.ExamPeriodID = ep.ExamPeriodID
    JOIN ExamTypes et ON ep.ExamTypeID = et.ExamTypeID
    WHERE et.ExamTypeName = 'Month';

    IF EXISTS(SELECT 1 FROM #temp)
    BEGIN
        SELECT TOP 1 @StudentID = StudentID, @SubjectID = SubjectID, @StageID = StageID, @AcademicYearID = AcademicYearID FROM #temp;

        EXEC sp_CalculateMonthGrades_ForStudentSubject
            @StudentID = @StudentID,
            @SubjectID = @SubjectID,
            @StageID = @StageID,
            @AcademicYearID = @AcademicYearID;
    END

    DROP TABLE #temp;
END;









