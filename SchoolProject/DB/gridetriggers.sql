alter TRIGGER trg_AfterInsertUpdate_Results
ON Results
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- ����� ��� ��� �� ��� inserted (���� ����� ���� ��� �����)
    DECLARE @StudentID INT, @ExamID INT;
    SELECT TOP 1 @StudentID = i.StudentID, @ExamID = i.ExamID FROM inserted i;

    -- ��� ������ �������� �������
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

    -- ���� ���� ������ (20%)
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    SELECT @AttendancePoints = 
        (COUNT(CASE WHEN Status = 'Present' THEN 1 END) * 20.0) /
        NULLIF(COUNT(*),0)
    FROM StudentAttendance
    WHERE StudentID = @StudentID
      AND AttendanceDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                             AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @AttendancePoints IS NULL SET @AttendancePoints = 0;

    -- ���� ���� �������� (40%)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;
    SELECT @DisciplinePoints = AVG(CAST(Points AS DECIMAL(5,2))) * 0.4
    FROM Discipline
    WHERE StudentID = @StudentID
      AND DisciplineDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                              AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @DisciplinePoints IS NULL SET @DisciplinePoints = 0;

    -- ���� ���� �������� (40%)
    DECLARE @ExamPoints DECIMAL(5,2) = 0;
    SELECT @ExamPoints = 
        (SUM(ISNULL(r.Score,0)) * 40.0) / NULLIF(SUM(ISNULL(e.MaxGrade,0)),0)
    FROM Results r
    INNER JOIN Exams e ON r.ExamID = e.ExamID
    WHERE r.StudentID = @StudentID
      AND e.SubjectID = @SubjectID
      AND e.ExamPeriodID = @ExamPeriodID;

    IF @ExamPoints IS NULL SET @ExamPoints = 0;

    -- ������� �������
    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    -- ����� �� ����� ����� �� ���� FinalGrades
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

    -- ���� ��� ��� ���� ��� �� inserted (���� ����� ���� ����)
    DECLARE @StudentID INT;
    SELECT TOP 1 @StudentID = i.StudentID FROM inserted i;

    -- ����� ����� ���� �������� ���� ����� ����� ������
    DECLARE @AttendanceDate DATE;
    SELECT TOP 1 @AttendanceDate = AttendanceDate FROM inserted;

    DECLARE @ExamPeriodID INT;
    SELECT TOP 1 @ExamPeriodID = ExamPeriodID 
    FROM ExamPeriods 
    WHERE @AttendanceDate BETWEEN StartDate AND EndDate;

    IF @ExamPeriodID IS NULL RETURN; -- �� ���� ���� ������

    -- ��� ������ ������ �� StudentStages ���� �������� ������
    DECLARE @StageID INT, @AcademicYearID INT;
    SELECT TOP 1 @StageID = ss.StageID, @AcademicYearID = ss.AcademicYearID 
    FROM StudentStages ss 
    WHERE ss.StudentID = @StudentID AND ss.IsActive = 1;

    -- ����� ���� ��� ���� ������ ��������� ��������� ��� �� Trigger �����

    -- ���� ���� ������ (20%)
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    SELECT @AttendancePoints = 
        (COUNT(CASE WHEN Status = 'Present' THEN 1 END) * 20.0) /
        NULLIF(COUNT(*),0)
    FROM StudentAttendance
    WHERE StudentID = @StudentID
      AND AttendanceDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                             AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @AttendancePoints IS NULL SET @AttendancePoints = 0;

    -- ���� �������� (���� ���� �� Trigger ��� ��������ء ��� ��� ��� ������)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;

    -- ���� �������� (40%) � �� ���� ���� ��� ���� ����� ��� ���� Results� ��� ��� ������
    DECLARE @ExamPoints DECIMAL(5,2) = 0;

    -- ������� ������� ��� �� ���� ����ѡ ��� �� ����� ����� ����
    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    -- ������ FinalGrades ����� ����� ������ ���� �������� ��� ����� ����� ����� ������ ���� ������ (����� ��� ExamTypeID = 1 ��������)
    -- �� ������� ������ ����� ������� ���� ������ ���� �������� ���� ����

    IF EXISTS (
        SELECT 1 FROM FinalGrades
        WHERE StudentID = @StudentID
          AND StageID = @StageID
          AND AcademicYearID = @AcademicYearID
          AND ExamPeriodID = @ExamPeriodID
          -- AND SubjectID = ? -- ����� ������� ��� �������
          -- AND ExamTypeID = ? -- ����� ������� ��� �������
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

    -- ���� ���� �������� (40%)
    DECLARE @DisciplinePoints DECIMAL(5,2) = 0;
    SELECT @DisciplinePoints = AVG(CAST(Points AS DECIMAL(5,2))) * 0.4
    FROM Discipline
    WHERE StudentID = @StudentID
      AND DisciplineDate BETWEEN (SELECT StartDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID)
                              AND (SELECT EndDate FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID);

    IF @DisciplinePoints IS NULL SET @DisciplinePoints = 0;

    -- ��� ��� ������ ��������� �����ǡ ��� ��� Trigger ��� ��������� ���
    DECLARE @AttendancePoints DECIMAL(5,2) = 0;
    DECLARE @ExamPoints DECIMAL(5,2) = 0;

    DECLARE @FinalGrade DECIMAL(5,2) = @AttendancePoints + @DisciplinePoints + @ExamPoints;

    -- ����� �� ����� �� ���� FinalGrades (���� ��������� ��� ������ ���� �������� ��� �� Trigger ������)
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








