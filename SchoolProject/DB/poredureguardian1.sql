-- 1. Insert
CREATE PROCEDURE sp_Guardian_Insert
    @PersonID INT,
    @Jobs NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Guardians (PersonID, Jobs, Notes)
    VALUES (@PersonID, @Jobs, @Notes);

    SELECT SCOPE_IDENTITY() AS NewGuardianID;
END
GO

-- 2. Update
CREATE PROCEDURE sp_Guardian_Update
    @GuardianID INT,
    @PersonID INT,
    @Jobs NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Guardians
    SET PersonID = @PersonID,
        Jobs = @Jobs,
        Notes = @Notes
    WHERE GuardianID = @GuardianID;
END
GO

-- 3. Delete
CREATE PROCEDURE sp_Guardian_Delete
    @GuardianID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Guardians
    WHERE GuardianID = @GuardianID;
END
GO

-- 4. Get By ID
CREATE PROCEDURE sp_Guardian_GetByID
    @GuardianID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Guardians
    WHERE GuardianID = @GuardianID;
END
GO

-- 5. Get By PersonID
CREATE PROCEDURE sp_Guardian_GetByPersonID
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Guardians
    WHERE PersonID = @PersonID;
END
GO

-- 6. Get All
CREATE PROCEDURE sp_Guardian_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Guardians;
END
GO

-- 7. Exists By PersonID
CREATE PROCEDURE sp_Guardian_ExistsByPersonID
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Guardians WHERE PersonID = @PersonID)
        SELECT 1 AS [Exists];
    ELSE
        SELECT 0 AS [Exists];
END
GO


























-- 1. Insert
CREATE PROCEDURE sp_FeeType_Insert
    @FeeName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @IsActive BIT
AS
BEGIN
    INSERT INTO FeeTypes (FeeName, Description, IsActive)
    VALUES (@FeeName, @Description, @IsActive);

    SELECT SCOPE_IDENTITY();
END
GO

-- 2. Update
CREATE PROCEDURE sp_FeeType_Update
    @FeeTypeID INT,
    @FeeName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @IsActive BIT
AS
BEGIN
    UPDATE FeeTypes
    SET FeeName = @FeeName,
        Description = @Description,
        IsActive = @IsActive
    WHERE FeeTypeID = @FeeTypeID;
END
GO

-- 3. Delete
CREATE PROCEDURE sp_FeeType_Delete
    @FeeTypeID INT
AS
BEGIN
    DELETE FROM FeeTypes WHERE FeeTypeID = @FeeTypeID;
END
GO

-- 4. Get By ID
CREATE PROCEDURE sp_FeeType_GetByID
    @FeeTypeID INT
AS
BEGIN
    SELECT * FROM FeeTypes WHERE FeeTypeID = @FeeTypeID;
END
GO

-- 5. Get All
CREATE PROCEDURE sp_FeeType_GetAll
AS
BEGIN
    SELECT * FROM FeeTypes;
END
GO

-- 6. Get Active Only
CREATE PROCEDURE sp_FeeType_GetActive
AS
BEGIN
    SELECT * FROM FeeTypes WHERE IsActive = 1;
END
GO































































-- 1. Insert
CREATE PROCEDURE sp_StageFeeItem_Insert
    @StageID INT,
    @FeeTypeID INT,
    @AcademicYearID INT,
    @Amount DECIMAL(18,2),
    @Description NCHAR(100) = NULL,
    @IsActive BIT
AS
BEGIN
    INSERT INTO StageFeeItems (StageID, FeeTypeID, AcademicYearID, Amount, Description, IsActive)
    VALUES (@StageID, @FeeTypeID, @AcademicYearID, @Amount, @Description, @IsActive);

    SELECT SCOPE_IDENTITY();
END
GO

-- 2. Update
CREATE PROCEDURE sp_StageFeeItem_Update
    @StageFeeItemID INT,
    @StageID INT,
    @FeeTypeID INT,
    @AcademicYearID INT,
    @Amount DECIMAL(18,2),
    @Description NCHAR(100) = NULL,
    @IsActive BIT
AS
BEGIN
    UPDATE StageFeeItems
    SET StageID = @StageID,
        FeeTypeID = @FeeTypeID,
        AcademicYearID = @AcademicYearID,
        Amount = @Amount,
        Description = @Description,
        IsActive = @IsActive
    WHERE StageFeeItemID = @StageFeeItemID;
END
GO

-- 3. Delete
CREATE PROCEDURE sp_StageFeeItem_Delete
    @StageFeeItemID INT
AS
BEGIN
    DELETE FROM StageFeeItems WHERE StageFeeItemID = @StageFeeItemID;
END
GO

-- 4. Get By ID
CREATE PROCEDURE sp_StageFeeItem_GetByID
    @StageFeeItemID INT
AS
BEGIN
    SELECT * FROM StageFeeItems WHERE StageFeeItemID = @StageFeeItemID;
END
GO

-- 5. Get All
CREATE PROCEDURE sp_StageFeeItem_GetAll
AS
BEGIN
    SELECT * FROM StageFeeItems;
END
GO

-- 6. Get Active
CREATE PROCEDURE sp_StageFeeItem_GetActive
AS
BEGIN
    SELECT * FROM StageFeeItems WHERE IsActive = 1;
END
GO



















-- 1. Insert
CREATE PROCEDURE sp_StudentFee_Insert
    @StudentID INT,
    @StageFeeItemID INT,
    @DueDate DATE,
    @Status NVARCHAR(10),
    @Notes NVARCHAR(500) = NULL,
    @TotalPaid DECIMAL(18, 2)
AS
BEGIN
    INSERT INTO StudentFees (StudentID, StageFeeItemID, DueDate, Status, Notes, TotalPaid)
    VALUES (@StudentID, @StageFeeItemID, @DueDate, @Status, @Notes, @TotalPaid);

    SELECT SCOPE_IDENTITY();
END
GO

-- 2. Update
CREATE PROCEDURE sp_StudentFee_Update
    @StudentFeeID INT,
    @StudentID INT,
    @StageFeeItemID INT,
    @DueDate DATE,
    @Status NVARCHAR(10),
    @Notes NVARCHAR(500) = NULL,
    @TotalPaid DECIMAL(18, 2)
AS
BEGIN
    UPDATE StudentFees
    SET StudentID = @StudentID,
        StageFeeItemID = @StageFeeItemID,
        DueDate = @DueDate,
        Status = @Status,
        Notes = @Notes,
        TotalPaid = @TotalPaid
    WHERE StudentFeeID = @StudentFeeID;
END
GO

-- 3. Delete
CREATE PROCEDURE sp_StudentFee_Delete
    @StudentFeeID INT
AS
BEGIN
    DELETE FROM StudentFees WHERE StudentFeeID = @StudentFeeID;
END
GO

-- 4. Get By ID
CREATE PROCEDURE sp_StudentFee_GetByID
    @StudentFeeID INT
AS
BEGIN
    SELECT * FROM StudentFees WHERE StudentFeeID = @StudentFeeID;
END
GO

-- 5. Get All
CREATE PROCEDURE sp_StudentFee_GetAll
AS
BEGIN
    SELECT * FROM StudentFees;
END
GO

-- 6. Get By StudentID
CREATE PROCEDURE sp_StudentFee_GetByStudentID
    @StudentID INT
AS
BEGIN
    SELECT * FROM StudentFees WHERE StudentID = @StudentID;
END
GO

-- 7. Get Unpaid (Status not 'Paid')
CREATE PROCEDURE sp_StudentFee_GetUnpaid
AS
BEGIN
    SELECT * FROM StudentFees WHERE Status <> 'Paid';
END
GO



































-- 1. Insert Payment
CREATE PROCEDURE sp_StudentPayment_Insert
    @StudentFeeID INT,
    @PaymentDate DATE,
    @AmountPaid DECIMAL(18,2),
    @PaymentMethod NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO studentPayments (StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes)
    VALUES (@StudentFeeID, @PaymentDate, @AmountPaid, @PaymentMethod, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- 2. Update Payment
CREATE PROCEDURE sp_StudentPayment_Update
    @PaymentID INT,
    @StudentFeeID INT,
    @PaymentDate DATE,
    @AmountPaid DECIMAL(18,2),
    @PaymentMethod NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE studentPayments
    SET StudentFeeID = @StudentFeeID,
        PaymentDate = @PaymentDate,
        AmountPaid = @AmountPaid,
        PaymentMethod = @PaymentMethod,
        Notes = @Notes
    WHERE PaymentID = @PaymentID;
END
GO

-- 3. Delete Payment
CREATE PROCEDURE sp_StudentPayment_Delete
    @PaymentID INT
AS
BEGIN
    DELETE FROM studentPayments WHERE PaymentID = @PaymentID;
END
GO

-- 4. Get Payment By ID
CREATE PROCEDURE sp_StudentPayment_GetByID
    @PaymentID INT
AS
BEGIN
    SELECT * FROM studentPayments WHERE PaymentID = @PaymentID;
END
GO

-- 5. Get All Payments
CREATE PROCEDURE sp_StudentPayment_GetAll
AS
BEGIN
    SELECT * FROM studentPayments;
END
GO

-- 6. Get Payments By StudentFeeID
CREATE PROCEDURE sp_StudentPayment_GetByStudentFeeID
    @StudentFeeID INT
AS
BEGIN
    SELECT * FROM studentPayments WHERE StudentFeeID = @StudentFeeID ORDER BY PaymentDate DESC;
END
GO
















-- Insert RoomType
CREATE PROCEDURE sp_RoomType_Insert
    @TypeName NVARCHAR(50),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT
AS
BEGIN
    INSERT INTO RoomTypes (TypeName, Description, IsActive)
    VALUES (@TypeName, @Description, @IsActive);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update RoomType
CREATE PROCEDURE sp_RoomType_Update
    @RoomTypeID INT,
    @TypeName NVARCHAR(50),
    @Description NVARCHAR(250) = NULL,
    @IsActive BIT
AS
BEGIN
    UPDATE RoomTypes
    SET TypeName = @TypeName,
        Description = @Description,
        IsActive = @IsActive
    WHERE RoomTypeID = @RoomTypeID;
END
GO

-- Delete RoomType
CREATE PROCEDURE sp_RoomType_Delete
    @RoomTypeID INT
AS
BEGIN
    DELETE FROM RoomTypes WHERE RoomTypeID = @RoomTypeID;
END
GO

-- Get By ID
CREATE PROCEDURE sp_RoomType_GetByID
    @RoomTypeID INT
AS
BEGIN
    SELECT * FROM RoomTypes WHERE RoomTypeID = @RoomTypeID;
END
GO

-- Get All
CREATE PROCEDURE sp_RoomType_GetAll
AS
BEGIN
    SELECT * FROM RoomTypes;
END
GO

-- Get Active Only
CREATE PROCEDURE sp_RoomType_GetActive
AS
BEGIN
    SELECT * FROM RoomTypes WHERE IsActive = 1;
END
GO














-- Insert Room
CREATE PROCEDURE sp_Room_Insert
    @RoomName NVARCHAR(100),
    @RoomTypeID INT,
    @Capacity INT,
    @Location NVARCHAR(100),
    @IsAvailable BIT,
    @IsReservable BIT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO Rooms (RoomName, RoomTypeID, Capacity, Location, IsAvailable, IsReservable, Notes)
    VALUES (@RoomName, @RoomTypeID, @Capacity, @Location, @IsAvailable, @IsReservable, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Room
CREATE PROCEDURE sp_Room_Update
    @RoomID INT,
    @RoomName NVARCHAR(100),
    @RoomTypeID INT,
    @Capacity INT,
    @Location NVARCHAR(100),
    @IsAvailable BIT,
    @IsReservable BIT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    UPDATE Rooms
    SET RoomName = @RoomName,
        RoomTypeID = @RoomTypeID,
        Capacity = @Capacity,
        Location = @Location,
        IsAvailable = @IsAvailable,
        IsReservable = @IsReservable,
        Notes = @Notes
    WHERE RoomID = @RoomID;
END
GO

-- Delete Room
CREATE PROCEDURE sp_Room_Delete
    @RoomID INT
AS
BEGIN
    DELETE FROM Rooms WHERE RoomID = @RoomID;
END
GO

-- Get Room By ID
CREATE PROCEDURE sp_Room_GetByID
    @RoomID INT
AS
BEGIN
    SELECT * FROM Rooms WHERE RoomID = @RoomID;
END
GO

-- Get All Rooms
CREATE PROCEDURE sp_Room_GetAll
AS
BEGIN
    SELECT * FROM Rooms;
END
GO

-- Get Available Rooms
CREATE PROCEDURE sp_Room_GetAvailable
AS
BEGIN
    SELECT * FROM Rooms WHERE IsAvailable = 1;
END
GO

























-- Insert
CREATE PROCEDURE sp_JobTitle_Insert
    @JobTitleName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @IsTeaching BIT,
    @IsAdministrative BIT,
    @CanTeach BIT,
    @RequiresCertification BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO JobTitles (JobTitleName, Description, IsTeaching, IsAdministrative, CanTeach, RequiresCertification)
    VALUES (@JobTitleName, @Description, @IsTeaching, @IsAdministrative, @CanTeach, @RequiresCertification);

    SELECT SCOPE_IDENTITY() AS NewJobTitleID;
END
GO

-- Update
CREATE PROCEDURE sp_JobTitle_Update
    @JobTitleID INT,
    @JobTitleName NVARCHAR(100),
    @Description NVARCHAR(255) = NULL,
    @IsTeaching BIT,
    @IsAdministrative BIT,
    @CanTeach BIT,
    @RequiresCertification BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE JobTitles
    SET JobTitleName = @JobTitleName,
        Description = @Description,
        IsTeaching = @IsTeaching,
        IsAdministrative = @IsAdministrative,
        CanTeach = @CanTeach,
        RequiresCertification = @RequiresCertification
    WHERE JobTitleID = @JobTitleID;
END
GO

-- Delete (return affected rows)
CREATE PROCEDURE sp_JobTitle_Delete
    @JobTitleID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM JobTitles WHERE JobTitleID = @JobTitleID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- GetByID
CREATE PROCEDURE sp_JobTitle_GetByID
    @JobTitleID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM JobTitles WHERE JobTitleID = @JobTitleID;
END
GO

-- GetAll
CREATE PROCEDURE sp_JobTitle_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM JobTitles;
END
GO



-- Get Active JobTitles (ãËáÇð ÇáÊí áíÓÊ ãÍÐæÝÉ áæ Ýí ÚãæÏ IsActive - áæ ãÇ Ýí íãßä ÊÌÇåá)
-- áæ ãÇ Ýí ÚãæÏ IsActive íãßäß ÍÐÝ ÇáÅÌÑÇÁ Ãæ ÊÚÏíáå
CREATE PROCEDURE sp_JobTitle_GetActive
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM JobTitles WHERE 1=1; -- ÚÏá ÍÓÈ ÍÇáÉ ÇáäÔÇØ áæ ãÊæÝÑÉ
END
GO










-- Insert Employee
CREATE PROCEDURE sp_Employee_Insert
    @PersonID INT,
    @JobTitleID INT,
    @HireDate DATE,
    @TerminationDate DATE = NULL,
    @IsActive BIT,
    @Notes NVARCHAR(500) = NULL,
    @Salary DECIMAL(18,0)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employees (PersonID, JobTitleID, HireDate, TerminationDate, IsActive, Notes, Salary)
    VALUES (@PersonID, @JobTitleID, @HireDate, @TerminationDate, @IsActive, @Notes, @Salary);

    SELECT SCOPE_IDENTITY() AS NewEmployeeID;
END
GO

alter PROCEDURE sp_EmployeeAttendance_Update
    @AttendanceID INT,
    @EmployeeID INT,
    @AttendanceDate DATE,
    @CheckInTime TIME = NULL,
    @CheckOutTime TIME = NULL,
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE EmployeeAttendance
    SET EmployeeID = @EmployeeID,
        AttendanceDate = @AttendanceDate,
        CheckInTime = @CheckInTime,
        CheckOutTime = @CheckOutTime,
        Status = @Status
    WHERE AttendanceID = @AttendanceID;

    -- ÃÑÌÚ ÚÏÏ ÇáÃÓØÑ ÇáãÊÃËÑÉ
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- Delete Employee ãÚ ÅÚÇÏÉ ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
CREATE PROCEDURE sp_Employee_Delete
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Employees WHERE EmployeeID = @EmployeeID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- Get Employee by ID
CREATE PROCEDURE sp_Employee_GetByID
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Employees WHERE EmployeeID = @EmployeeID;
END
GO

-- Get All Employees
CREATE PROCEDURE sp_Employee_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Employees;
END
GO

-- Get Active Employees ÝÞØ
CREATE PROCEDURE sp_Employee_GetActive
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Employees WHERE IsActive = 1;
END
GO































































-- Insert
CREATE PROCEDURE sp_EmployeeAttendance_Insert
    @EmployeeID INT,
    @AttendanceDate DATE,
    @CheckInTime TIME(7) = NULL,
    @CheckOutTime TIME(7) = NULL,
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO EmployeeAttendance (EmployeeID, AttendanceDate, CheckInTime, CheckOutTime, Status)
    VALUES (@EmployeeID, @AttendanceDate, @CheckInTime, @CheckOutTime, @Status);

    SELECT SCOPE_IDENTITY() AS NewAttendanceID;
END
GO

-- Update
CREATE PROCEDURE sp_EmployeeAttendance_Update
    @AttendanceID INT,
    @EmployeeID INT,
    @AttendanceDate DATE,
    @CheckInTime TIME(7) = NULL,
    @CheckOutTime TIME(7) = NULL,
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE EmployeeAttendance
    SET EmployeeID = @EmployeeID,
        AttendanceDate = @AttendanceDate,
        CheckInTime = @CheckInTime,
        CheckOutTime = @CheckOutTime,
        Status = @Status
    WHERE AttendanceID = @AttendanceID;
END
GO

-- Delete (ãÚ ÅÚÇÏÉ ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ)
CREATE PROCEDURE sp_EmployeeAttendance_Delete
    @AttendanceID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM EmployeeAttendance
    WHERE AttendanceID = @AttendanceID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- Get By ID
CREATE PROCEDURE sp_EmployeeAttendance_GetByID
    @AttendanceID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance
    WHERE AttendanceID = @AttendanceID;
END
GO

-- Get All
CREATE PROCEDURE sp_EmployeeAttendance_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance;
END
GO

-- Check if Active (exists for employee on specific date)
CREATE PROCEDURE sp_EmployeeAttendance_Exists
    @EmployeeID INT,
    @AttendanceDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 FROM EmployeeAttendance WHERE EmployeeID = @EmployeeID AND AttendanceDate = @AttendanceDate
    )
        SELECT 1 AS ExistsFlag;
    ELSE
        SELECT 0 AS ExistsFlag;
END
GO


-- 1. Get Attendance By EmployeeID
CREATE PROCEDURE sp_EmployeeAttendance_GetByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance
    WHERE EmployeeID = @EmployeeID
    ORDER BY AttendanceDate DESC;
END
GO

-- 2. Get Attendance By EmployeeID Between Dates
CREATE PROCEDURE sp_EmployeeAttendance_GetByEmployeeIDBetweenDates
    @EmployeeID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance
    WHERE EmployeeID = @EmployeeID
      AND AttendanceDate BETWEEN @StartDate AND @EndDate
    ORDER BY AttendanceDate DESC;
END
GO

-- 3. Get Attendance By Status
CREATE PROCEDURE sp_EmployeeAttendance_GetByStatus
    @Status NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance
    WHERE Status = @Status
    ORDER BY AttendanceDate DESC;
END
GO

-- 4. Get Attendance By Status Between Dates
CREATE PROCEDURE sp_EmployeeAttendance_GetByStatusBetweenDates
    @Status NVARCHAR(50),
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM EmployeeAttendance
    WHERE Status = @Status
      AND AttendanceDate BETWEEN @StartDate AND @EndDate
    ORDER BY AttendanceDate DESC;
END
GO









-- Insert Student
CREATE PROCEDURE sp_Student_Insert
    @PersonID INT,
    @GuardianID INT,
    @EnrollmentDate DATE,
    @StageID INT,
    @AcademicYearID INT,
    @IsActive BIT,
    @DocumentPath NVARCHAR(512) = NULL,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Students (PersonID, GuardianID, EnrollmentDate, StageID, AcademicYearID, IsActive, DocumentPath, Notes)
    VALUES (@PersonID, @GuardianID, @EnrollmentDate, @StageID, @AcademicYearID, @IsActive, @DocumentPath, @Notes);

    SELECT SCOPE_IDENTITY() AS NewStudentID;
END
GO

-- Update Student
alter PROCEDURE sp_Student_Update
    @StudentID INT,
    @PersonID INT,
    @GuardianID INT,
    @EnrollmentDate DATE,
    @StageID INT,
    @AcademicYearID INT,
    @IsActive BIT,
    @DocumentPath NVARCHAR(512) = NULL,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    -- Þã ÈÅáÛÇÁ åÐÇ ÇáÓØÑ ßí íÊã ÅÑÌÇÚ ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
    -- SET NOCOUNT ON;

    UPDATE Students
    SET PersonID = @PersonID,
        GuardianID = @GuardianID,
        EnrollmentDate = @EnrollmentDate,
        StageID = @StageID,
        AcademicYearID = @AcademicYearID,
        IsActive = @IsActive,
        DocumentPath = @DocumentPath,
        Notes = @Notes
    WHERE StudentID = @StudentID;
END
GO

-- Delete Student - return affected rows
CREATE PROCEDURE sp_Student_Delete
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Students WHERE StudentID = @StudentID;

    SELECT @@ROWCOUNT AS RowsAffected;
END
GO

-- Get Student By ID
CREATE PROCEDURE sp_Student_GetByID
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Students WHERE StudentID = @StudentID;
END
GO

-- Get All Students
CREATE PROCEDURE sp_Student_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * FROM Students;
END
GO

-- Update Student IsActive
alter PROCEDURE sp_Student_UpdateIsActive
    @StudentID INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT OFF;

    UPDATE Students
    SET IsActive = @IsActive
    WHERE StudentID = @StudentID;

    -- íÑÌÚ ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
    RETURN @@ROWCOUNT;
END





CREATE PROCEDURE sp_Student_GetByStage
    @StageID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Students WHERE StageID = @StageID;
END
GO

CREATE PROCEDURE sp_Student_GetByGuardianID
    @GuardianID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Students WHERE GuardianID = @GuardianID;
END
GO


CREATE PROCEDURE sp_Student_GetByAcademicYear
    @AcademicYearID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Students WHERE AcademicYearID = @AcademicYearID;
END
















-- Insert
CREATE PROCEDURE sp_StudentAttendance_Insert
    @StudentID INT,
    @ScheduleID INT,
    @AttendanceDate DATE,
    @Status NVARCHAR(20),
    @Notes NVARCHAR(500)
AS
BEGIN
    INSERT INTO StudentAttendance(StudentID, ScheduleID, AttendanceDate, Status, Notes)
    VALUES(@StudentID, @ScheduleID, @AttendanceDate, @Status, @Notes);

    SELECT SCOPE_IDENTITY();
    END
    GO

-- Update
CREATE PROCEDURE sp_StudentAttendance_Update
    @AttendanceID INT,
    @StudentID INT,
    @ScheduleID INT,
    @AttendanceDate DATE,
    @Status NVARCHAR(20),
    @Notes NVARCHAR(500)
AS
BEGIN
    UPDATE StudentAttendance
    SET StudentID = @StudentID,
        ScheduleID = @ScheduleID,
        AttendanceDate = @AttendanceDate,
        Status = @Status,
        Notes = @Notes
    WHERE AttendanceID = @AttendanceID;
    END
    GO

-- Delete
CREATE PROCEDURE sp_StudentAttendance_Delete
    @AttendanceID INT
AS
BEGIN
    DELETE FROM StudentAttendance WHERE AttendanceID = @AttendanceID;
    SELECT @@ROWCOUNT;
    END
    GO

-- GetByID
CREATE PROCEDURE sp_StudentAttendance_GetByID
    @AttendanceID INT
AS
BEGIN
    SELECT* FROM StudentAttendance WHERE AttendanceID = @AttendanceID;
END
GO

-- GetAll
CREATE PROCEDURE sp_StudentAttendance_GetAll
AS
BEGIN
    SELECT* FROM StudentAttendance;
END
GO

-- GetByStudentID
CREATE PROCEDURE sp_StudentAttendance_GetByStudentID
    @StudentID INT
AS
BEGIN
    SELECT* FROM StudentAttendance WHERE StudentID = @StudentID;
END
GO

-- GetByDateRange
CREATE PROCEDURE sp_StudentAttendance_GetByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT* FROM StudentAttendance WHERE AttendanceDate BETWEEN @StartDate AND @EndDate;
END
GO

-- GetByStatus
CREATE PROCEDURE sp_StudentAttendance_GetByStatus
    @Status NVARCHAR(20)
AS
BEGIN
    SELECT* FROM StudentAttendance WHERE Status = @Status;
END
GO










CREATE PROCEDURE sp_StudentAttendance_Exists
    @StudentID INT,
    @ScheduleID INT,
    @AttendanceDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1
        FROM StudentAttendance
        WHERE StudentID = @StudentID
          AND ScheduleID = @ScheduleID
          AND AttendanceDate = @AttendanceDate
    )
        SELECT 1;
    ELSE
        SELECT 0;
END


CREATE PROCEDURE sp_StudentAttendance_IsRelated
    @AttendanceID INT,
    @IsRelated BIT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- ãËÇá: ÊÍÞÞ ÅÐÇ ßÇä íæÌÏ ÓÌá ãÑÊÈØ Ýí ÌÏæá ÂÎÑ
    -- íãßä ÊÚÏíá ÍÓÈ ÞÇÚÏÉ ÇáÈíÇäÇÊ áÏíß
    IF EXISTS (
        SELECT 1
        FROM SomeOtherTable
        WHERE AttendanceID = @AttendanceID
    )
    BEGIN
        SET @IsRelated = 1;
    END
    ELSE
    BEGIN
        SET @IsRelated = 0;
    END
END








CREATE PROCEDURE sp_Stage_Insert
    @StageName NVARCHAR(50),
    @IsActive BIT
AS
BEGIN
    INSERT INTO Stages (StageName, IsActive)
    VALUES (@StageName, @IsActive);

    SELECT SCOPE_IDENTITY(); -- Return the new ID
END


CREATE PROCEDURE sp_Stage_Update
    @StageID INT,
    @StageName NVARCHAR(50),
    @IsActive BIT
AS
BEGIN
    UPDATE Stages
    SET StageName = @StageName,
        IsActive = @IsActive
    WHERE StageID = @StageID;
END



CREATE PROCEDURE sp_Stage_Delete
    @StageID INT
AS
BEGIN
    DELETE FROM Stages
    WHERE StageID = @StageID;

    SELECT @@ROWCOUNT; -- Return affected rows
END




CREATE PROCEDURE sp_Stage_GetByID
    @StageID INT
AS
BEGIN
    SELECT * FROM Stages WHERE StageID = @StageID;
END


CREATE PROCEDURE sp_Stage_GetAll
AS
BEGIN
    SELECT * FROM Stages;
END




CREATE PROCEDURE sp_Stage_GetActive
AS
BEGIN
    SELECT * FROM Stages WHERE IsActive = 1;
END







-- Insert Class
CREATE PROCEDURE sp_Class_Insert
    @ClassName NVARCHAR(100),
    @StageID INT,
    @Capacity INT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO Classes (ClassName, StageID, Capacity, Notes)
    VALUES (@ClassName, @StageID, @Capacity, @Notes);

    SELECT SCOPE_IDENTITY(); -- ÊÑÌÚ ÇáÜ ClassID ÇáÌÏíÏ
END
GO

-- Update Class
CREATE PROCEDURE sp_Class_Update
    @ClassID INT,
    @ClassName NVARCHAR(100),
    @StageID INT,
    @Capacity INT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Classes
    SET ClassName = @ClassName,
        StageID = @StageID,
        Capacity = @Capacity,
        Notes = @Notes
    WHERE ClassID = @ClassID;

    SELECT @@ROWCOUNT; -- ÚÏÏ ÇáÕÝæÝ ÇáãÊÃËÑÉ
END
GO

-- Delete Class
CREATE PROCEDURE sp_Class_Delete
    @ClassID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Classes WHERE ClassID = @ClassID;

    SELECT @@ROWCOUNT; -- ÚÏÏ ÇáÕÝæÝ ÇáãÍÐæÝÉ
END
GO

-- Get Class By ID
CREATE PROCEDURE sp_Class_GetByID
    @ClassID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ClassID, ClassName, StageID, Capacity, Notes
    FROM Classes
    WHERE ClassID = @ClassID;
END
GO

-- Get All Classes
CREATE PROCEDURE sp_Class_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT ClassID, ClassName, StageID, Capacity, Notes
    FROM Classes
    ORDER BY ClassName;
END
GO






























-- 1. Insert new salary record (ÈÏæä NetSalary áÃäå ãÍÓæÈ)
CREATE PROCEDURE sp_MonthlySalary_Insert
    @EmployeeID INT,
    @SalaryMonth DATE,
    @BaseSalary DECIMAL(18,2),
    @AbsenceDays INT,
    @DeductionPerAbsent DECIMAL(18,2),
    @Paid BIT,
    @PaymentDate DATE = NULL
AS
BEGIN
    INSERT INTO MonthlySalaries 
    (EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate)
    VALUES 
    (@EmployeeID, @SalaryMonth, @BaseSalary, @AbsenceDays, @DeductionPerAbsent, @Paid, @PaymentDate);

    SELECT SCOPE_IDENTITY() AS NewSalaryID;
END
GO


-- 2. Update existing salary record (ÈÏæä NetSalary)
CREATE PROCEDURE sp_MonthlySalary_Update
    @SalaryID INT,
    @EmployeeID INT,
    @SalaryMonth DATE,
    @BaseSalary DECIMAL(18,2),
    @AbsenceDays INT,
    @DeductionPerAbsent DECIMAL(18,2),
    @Paid BIT,
    @PaymentDate DATE = NULL
AS
BEGIN
    UPDATE MonthlySalaries
    SET EmployeeID = @EmployeeID,
        SalaryMonth = @SalaryMonth,
        BaseSalary = @BaseSalary,
        AbsenceDays = @AbsenceDays,
        DeductionPerAbsent = @DeductionPerAbsent,
        Paid = @Paid,
        PaymentDate = @PaymentDate
    WHERE SalaryID = @SalaryID;
END
GO


-- 3. Delete salary record
CREATE PROCEDURE sp_MonthlySalary_Delete
    @SalaryID INT
AS
BEGIN
    DELETE FROM MonthlySalaries WHERE SalaryID = @SalaryID;
END
GO


-- 4. Get salary record by SalaryID
CREATE PROCEDURE sp_MonthlySalary_GetByID
    @SalaryID INT
AS
BEGIN
    SELECT * FROM MonthlySalaries WHERE SalaryID = @SalaryID;
END
GO


-- 5. Get all salary records
CREATE PROCEDURE sp_MonthlySalary_GetAll
AS
BEGIN
    SELECT * FROM MonthlySalaries;
END
GO


-- 6. Get salary record by EmployeeID and Month
CREATE PROCEDURE sp_MonthlySalary_GetByEmployeeAndMonth
    @EmployeeID INT,
    @SalaryMonth DATE
AS
BEGIN
    SELECT * FROM MonthlySalaries
    WHERE EmployeeID = @EmployeeID 
      AND MONTH(SalaryMonth) = MONTH(@SalaryMonth) 
      AND YEAR(SalaryMonth) = YEAR(@SalaryMonth);
END
GO


-- 7. Get salary sums by month (ÚÏÏ ÇáÑæÇÊÈ æÇáãÌãæÚÇÊ)
CREATE PROCEDURE sp_MonthlySalary_GetSumsByMonth
    @SalaryMonth DATE
AS
BEGIN
    SELECT
        COUNT(*) AS TotalSalaries,
        SUM(NetSalary) AS TotalNetSalary,
        SUM(CASE WHEN Paid = 1 THEN NetSalary ELSE 0 END) AS TotalPaid,
        SUM(CASE WHEN Paid = 0 THEN NetSalary ELSE 0 END) AS TotalUnpaid
    FROM MonthlySalaries
    WHERE MONTH(SalaryMonth) = MONTH(@SalaryMonth) 
      AND YEAR(SalaryMonth) = YEAR(@SalaryMonth);
END
GO


-- 8. Get all salary records by EmployeeID, ordered descending by month
CREATE PROCEDURE sp_MonthlySalary_GetAllByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SELECT 
        SalaryID,
        EmployeeID,
        SalaryMonth,
        BaseSalary,
        AbsenceDays,
        DeductionPerAbsent,
        NetSalary,
        Paid,
        PaymentDate
    FROM MonthlySalaries
    WHERE EmployeeID = @EmployeeID
    ORDER BY SalaryMonth DESC;
END
GO


-- 9. Update only Paid status and PaymentDate
CREATE PROCEDURE sp_MonthlySalary_UpdatePaidStatus
    @SalaryID INT,
    @Paid BIT,
    @PaymentDate DATE = NULL
AS
BEGIN
    UPDATE MonthlySalaries
    SET Paid = @Paid,
        PaymentDate = CASE WHEN @Paid = 1 THEN ISNULL(@PaymentDate, GETDATE()) ELSE NULL END
    WHERE SalaryID = @SalaryID;
    
    SELECT @@ROWCOUNT AS RowsAffected;
END
GO











CREATE PROCEDURE sp_MonthlySalary_GetLastByEmployee
    @EmployeeID INT
AS
BEGIN
    SELECT TOP 1 *
    FROM MonthlySalaries
    WHERE EmployeeID = @EmployeeID
    ORDER BY SalaryMonth DESC;
END
GO
CREATE PROCEDURE sp_MonthlySalary_GetSummaryByEmployeeAndPeriod
    @EmployeeID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT
        COUNT(*) AS SalaryCount,
        SUM(NetSalary) AS TotalNetSalary,
        SUM(CASE WHEN Paid = 1 THEN NetSalary ELSE 0 END) AS TotalPaid,
        SUM(CASE WHEN Paid = 0 THEN NetSalary ELSE 0 END) AS TotalUnpaid
    FROM MonthlySalaries
    WHERE EmployeeID = @EmployeeID
      AND SalaryMonth BETWEEN @StartDate AND @EndDate;
END
GO
CREATE PROCEDURE sp_MonthlySalary_GetUnpaidByMonth
    @SalaryMonth DATE
AS
BEGIN
    SELECT *
    FROM MonthlySalaries
    WHERE Paid = 0
      AND MONTH(SalaryMonth) = MONTH(@SalaryMonth)
      AND YEAR(SalaryMonth) = YEAR(@SalaryMonth);
END
GO
CREATE PROCEDURE sp_MonthlySalary_PartialUpdate
    @SalaryID INT,
    @BaseSalary DECIMAL(18,2) = NULL,
    @AbsenceDays INT = NULL,
    @DeductionPerAbsent DECIMAL(18,2) = NULL,
    @Paid BIT = NULL,
    @PaymentDate DATE = NULL
AS
BEGIN
    UPDATE MonthlySalaries
    SET
        BaseSalary = COALESCE(@BaseSalary, BaseSalary),
        AbsenceDays = COALESCE(@AbsenceDays, AbsenceDays),
        DeductionPerAbsent = COALESCE(@DeductionPerAbsent, DeductionPerAbsent),
        Paid = COALESCE(@Paid, Paid),
        PaymentDate = CASE WHEN @Paid = 1 THEN COALESCE(@PaymentDate, PaymentDate) ELSE NULL END
    WHERE SalaryID = @SalaryID;
END
GO
CREATE PROCEDURE sp_MonthlySalary_GetAverageNetSalaryByPeriod
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT 
        AVG(BaseSalary - (AbsenceDays * DeductionPerAbsent)) AS AverageNetSalary
    FROM MonthlySalaries
    WHERE SalaryMonth BETWEEN @StartDate AND @EndDate;
END
GO
CREATE PROCEDURE sp_MonthlySalary_GetMonthsWithUnpaid
AS
BEGIN
    SELECT DISTINCT 
        YEAR(SalaryMonth) AS Year,
        MONTH(SalaryMonth) AS Month
    FROM MonthlySalaries
    WHERE Paid = 0
    ORDER BY Year DESC, Month DESC;
END
GO
CREATE PROCEDURE sp_MonthlySalary_GetTotalDeductionByEmployeePeriod
    @EmployeeID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT
        SUM(AbsenceDays * DeductionPerAbsent) AS TotalDeductions
    FROM MonthlySalaries
    WHERE EmployeeID = @EmployeeID
      AND SalaryMonth BETWEEN @StartDate AND @EndDate;
END
GO































-- Insert Booking
CREATE PROCEDURE sp_Booking_Insert
    @RoomID INT,
    @EmployeeID INT,
    @Purpose NVARCHAR(100),
    @BookingDate DATE,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Status NVARCHAR(20),
    @Notes NVARCHAR(500) = NULL,
    @RecurrenceType NVARCHAR(50) = NULL,
    @RecurrenceEndDate DATE = NULL
AS
BEGIN
    INSERT INTO Bookings
    (RoomID, EmployeeID, Purpose, BookingDate, StartTime, EndTime, Status, Notes, RecurrenceType, RecurrenceEndDate)
    VALUES
    (@RoomID, @EmployeeID, @Purpose, @BookingDate, @StartTime, @EndTime, @Status, @Notes, @RecurrenceType, @RecurrenceEndDate);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Booking
CREATE PROCEDURE sp_Booking_Update
    @BookingID INT,
    @RoomID INT,
    @EmployeeID INT,
    @Purpose NVARCHAR(100),
    @BookingDate DATE,
    @StartTime DATETIME,
    @EndTime DATETIME,
    @Status NVARCHAR(20),
    @Notes NVARCHAR(500) = NULL,
    @RecurrenceType NVARCHAR(50) = NULL,
    @RecurrenceEndDate DATE = NULL
AS
BEGIN
    UPDATE Bookings
    SET RoomID = @RoomID,
        EmployeeID = @EmployeeID,
        Purpose = @Purpose,
        BookingDate = @BookingDate,
        StartTime = @StartTime,
        EndTime = @EndTime,
        Status = @Status,
        Notes = @Notes,
        RecurrenceType = @RecurrenceType,
        RecurrenceEndDate = @RecurrenceEndDate
    WHERE BookingID = @BookingID;
END
GO

-- Delete Booking
CREATE PROCEDURE sp_Booking_Delete
    @BookingID INT
AS
BEGIN
    DELETE FROM Bookings WHERE BookingID = @BookingID;
END
GO

-- Get Booking By ID
CREATE PROCEDURE sp_Booking_GetByID
    @BookingID INT
AS
BEGIN
    SELECT * FROM Bookings WHERE BookingID = @BookingID;
END
GO

-- Get All Bookings
CREATE PROCEDURE sp_Booking_GetAll
AS
BEGIN
    SELECT * FROM Bookings ORDER BY BookingDate DESC, StartTime;
END
GO

-- Get Bookings By RoomID
CREATE PROCEDURE sp_Booking_GetByRoomID
    @RoomID INT
AS
BEGIN
    SELECT * FROM Bookings WHERE RoomID = @RoomID ORDER BY BookingDate DESC, StartTime;
END
GO

-- Get Bookings By EmployeeID
CREATE PROCEDURE sp_Booking_GetByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Bookings WHERE EmployeeID = @EmployeeID ORDER BY BookingDate DESC, StartTime;
END
GO

















-- Insert Schedule
CREATE PROCEDURE sp_Schedule_Insert
    @ClassID INT,
    @SubjectID INT,
    @TeacherID INT,
    @RoomID INT,
    @DayOfWeek NVARCHAR(10),
    @StartTime TIME(7),
    @EndTime TIME(7),
    @AcademicYearID INT,
    @Notes NVARCHAR(300) = NULL
AS
BEGIN
    INSERT INTO Schedules
    (ClassID, SubjectID, TeacherID, RoomID, DayOfWeek, StartTime, EndTime, AcademicYearID, Notes)
    VALUES
    (@ClassID, @SubjectID, @TeacherID, @RoomID, @DayOfWeek, @StartTime, @EndTime, @AcademicYearID, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Schedule
CREATE PROCEDURE sp_Schedule_Update
    @ScheduleID INT,
    @ClassID INT,
    @SubjectID INT,
    @TeacherID INT,
    @RoomID INT,
    @DayOfWeek NVARCHAR(10),
    @StartTime TIME(7),
    @EndTime TIME(7),
    @AcademicYearID INT,
    @Notes NVARCHAR(300) = NULL
AS
BEGIN
    UPDATE Schedules
    SET ClassID = @ClassID,
        SubjectID = @SubjectID,
        TeacherID = @TeacherID,
        RoomID = @RoomID,
        DayOfWeek = @DayOfWeek,
        StartTime = @StartTime,
        EndTime = @EndTime,
        AcademicYearID = @AcademicYearID,
        Notes = @Notes
    WHERE ScheduleID = @ScheduleID;
END
GO

-- Delete Schedule
CREATE PROCEDURE sp_Schedule_Delete
    @ScheduleID INT
AS
BEGIN
    DELETE FROM Schedules WHERE ScheduleID = @ScheduleID;
END
GO

-- Get Schedule By ID
CREATE PROCEDURE sp_Schedule_GetByID
    @ScheduleID INT
AS
BEGIN
    SELECT * FROM Schedules WHERE ScheduleID = @ScheduleID;
END
GO

-- Get All Schedules
CREATE PROCEDURE sp_Schedule_GetAll
AS
BEGIN
    SELECT * FROM Schedules ORDER BY DayOfWeek, StartTime;
END
GO

-- Get Schedules By ClassID
CREATE PROCEDURE sp_Schedule_GetByClassID
    @ClassID INT
AS
BEGIN
    SELECT * FROM Schedules WHERE ClassID = @ClassID ORDER BY DayOfWeek, StartTime;
END
GO

-- Get Schedules By TeacherID
CREATE PROCEDURE sp_Schedule_GetByTeacherID
    @TeacherID INT
AS
BEGIN
    SELECT * FROM Schedules WHERE TeacherID = @TeacherID ORDER BY DayOfWeek, StartTime;
END
GO

-- Get Schedules By AcademicYearID
CREATE PROCEDURE sp_Schedule_GetByAcademicYearID
    @AcademicYearID INT
AS
BEGIN
    SELECT * FROM Schedules WHERE AcademicYearID = @AcademicYearID ORDER BY DayOfWeek, StartTime;
END
GO























-- Insert Subject
CREATE PROCEDURE sp_Subject_Insert
    @SubjectName NVARCHAR(50),
    @StageID INT,
    @IsActive BIT,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO Subjects (SubjectName, StageID, IsActive, Notes)
    VALUES (@SubjectName, @StageID, @IsActive, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Subject
CREATE PROCEDURE sp_Subject_Update
    @SubjectID INT,
    @SubjectName NVARCHAR(50),
    @StageID INT,
    @IsActive BIT,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE Subjects
    SET SubjectName = @SubjectName,
        StageID = @StageID,
        IsActive = @IsActive,
        Notes = @Notes
    WHERE SubjectID = @SubjectID;
END
GO

-- Delete Subject
CREATE PROCEDURE sp_Subject_Delete
    @SubjectID INT
AS
BEGIN
    DELETE FROM Subjects WHERE SubjectID = @SubjectID;
END
GO

-- Get Subject By ID
CREATE PROCEDURE sp_Subject_GetByID
    @SubjectID INT
AS
BEGIN
    SELECT * FROM Subjects WHERE SubjectID = @SubjectID;
END
GO

-- Get All Subjects
CREATE PROCEDURE sp_Subject_GetAll
AS
BEGIN
    SELECT * FROM Subjects ORDER BY SubjectName;
END
GO

-- Get Active Subjects
alter PROCEDURE sp_Subject_GetActive
AS
BEGIN
    SELECT * FROM Subjects WHERE IsActive = 1
END
GO

-- Get Subjects By StageID
CREATE PROCEDURE sp_Subject_GetByStageID
    @StageID INT
AS
BEGIN
    SELECT * FROM Subjects WHERE StageID = @StageID ORDER BY SubjectName;
END
GO





























-- Insert Exam
CREATE PROCEDURE sp_Exam_Insert
    @SubjectID INT,
    @TeacherID INT,
    @ExamDate DATE,
    @TakeDate DATE,
    @Grade SMALLINT,
    @MaxGrade SMALLINT,
    @ExamDocument NVARCHAR(255) = NULL,
    @Notes NVARCHAR(500) = NULL,
    @ExamPeriodID INT,
    @IsActive BIT
AS
BEGIN
    INSERT INTO Exams (SubjectID, TeacherID, ExamDate, TakeDate, Grade, MaxGrade, ExamDocument, Notes, ExamPeriodID, IsActive)
    VALUES (@SubjectID, @TeacherID, @ExamDate, @TakeDate, @Grade, @MaxGrade, @ExamDocument, @Notes, @ExamPeriodID, @IsActive);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Exam
CREATE PROCEDURE sp_Exam_Update
    @ExamID INT,
    @SubjectID INT,
    @TeacherID INT,
    @ExamDate DATE,
    @TakeDate DATE,
    @Grade SMALLINT,
    @MaxGrade SMALLINT,
    @ExamDocument NVARCHAR(255) = NULL,
    @Notes NVARCHAR(500) = NULL,
    @ExamPeriodID INT,
    @IsActive BIT
AS
BEGIN
    UPDATE Exams
    SET SubjectID = @SubjectID,
        TeacherID = @TeacherID,
        ExamDate = @ExamDate,
        TakeDate = @TakeDate,
        Grade = @Grade,
        MaxGrade = @MaxGrade,
        ExamDocument = @ExamDocument,
        Notes = @Notes,
        ExamPeriodID = @ExamPeriodID,
        IsActive = @IsActive
    WHERE ExamID = @ExamID;
END
GO

-- Delete Exam
CREATE PROCEDURE sp_Exam_Delete
    @ExamID INT
AS
BEGIN
    DELETE FROM Exams WHERE ExamID = @ExamID;
END
GO

-- Get Exam By ID
CREATE PROCEDURE sp_Exam_GetByID
    @ExamID INT
AS
BEGIN
    SELECT * FROM Exams WHERE ExamID = @ExamID;
END
GO

-- Get All Exams
CREATE PROCEDURE sp_Exam_GetAll
AS
BEGIN
    SELECT * FROM Exams ORDER BY ExamDate DESC;
END
GO

-- Get Active Exams
CREATE PROCEDURE sp_Exam_GetActive
AS
BEGIN
    SELECT * FROM Exams WHERE IsActive = 1 ORDER BY ExamDate DESC;
END
GO

-- Get Exams By SubjectID
CREATE PROCEDURE sp_Exam_GetBySubjectID
    @SubjectID INT
AS
BEGIN
    SELECT * FROM Exams WHERE SubjectID = @SubjectID ORDER BY ExamDate DESC;
END
GO














-- Insert Result
CREATE PROCEDURE sp_Result_Insert
    @StudentID INT,
    @ExamID INT,
    @Score DECIMAL(5,2) = NULL,
    @IsAbsent BIT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    INSERT INTO Results (StudentID, ExamID, Score, IsAbsent, Notes)
    VALUES (@StudentID, @ExamID, @Score, @IsAbsent, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Result
CREATE PROCEDURE sp_Result_Update
    @ResultID INT,
    @StudentID INT,
    @ExamID INT,
    @Score DECIMAL(5,2) = NULL,
    @IsAbsent BIT,
    @Notes NVARCHAR(500) = NULL
AS
BEGIN
    UPDATE Results
    SET StudentID = @StudentID,
        ExamID = @ExamID,
        Score = @Score,
        IsAbsent = @IsAbsent,
        Notes = @Notes
    WHERE ResultID = @ResultID;
END
GO

-- Delete Result
CREATE PROCEDURE sp_Result_Delete
    @ResultID INT
AS
BEGIN
    DELETE FROM Results WHERE ResultID = @ResultID;
END
GO

-- Get Result By ID
CREATE PROCEDURE sp_Result_GetByID
    @ResultID INT
AS
BEGIN
    SELECT * FROM Results WHERE ResultID = @ResultID;
END
GO

-- Get Results By StudentID
CREATE PROCEDURE sp_Result_GetByStudentID
    @StudentID INT
AS
BEGIN
    SELECT * FROM Results WHERE StudentID = @StudentID ORDER BY ResultID DESC;
END
GO

-- Get Results By ExamID
CREATE PROCEDURE sp_Result_GetByExamID
    @ExamID INT
AS
BEGIN
    SELECT * FROM Results WHERE ExamID = @ExamID ORDER BY ResultID DESC;
END
GO

-- Get All Results
CREATE PROCEDURE sp_Result_GetAll
AS
BEGIN
    SELECT * FROM Results ORDER BY ResultID DESC;
END
GO





-- Insert ExamPeriod
CREATE PROCEDURE sp_ExamPeriod_Insert
    @ExamPeriodName NVARCHAR(50),
    @ExamTypeID INT,
    @AcademicYearID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    INSERT INTO ExamPeriods (ExamPeriodName, ExamTypeID, AcademicYearID, StartDate, EndDate)
    VALUES (@ExamPeriodName, @ExamTypeID, @AcademicYearID, @StartDate, @EndDate);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update ExamPeriod
CREATE PROCEDURE sp_ExamPeriod_Update
    @ExamPeriodID INT,
    @ExamPeriodName NVARCHAR(50),
    @ExamTypeID INT,
    @AcademicYearID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    UPDATE ExamPeriods
    SET ExamPeriodName = @ExamPeriodName,
        ExamTypeID = @ExamTypeID,
        AcademicYearID = @AcademicYearID,
        StartDate = @StartDate,
        EndDate = @EndDate
    WHERE ExamPeriodID = @ExamPeriodID;
END
GO

-- Delete ExamPeriod
CREATE PROCEDURE sp_ExamPeriod_Delete
    @ExamPeriodID INT
AS
BEGIN
    DELETE FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID;
END
GO

-- Get ExamPeriod By ID
CREATE PROCEDURE sp_ExamPeriod_GetByID
    @ExamPeriodID INT
AS
BEGIN
    SELECT * FROM ExamPeriods WHERE ExamPeriodID = @ExamPeriodID;
END
GO

-- Get All ExamPeriods
CREATE PROCEDURE sp_ExamPeriod_GetAll
AS
BEGIN
    SELECT * FROM ExamPeriods ORDER BY StartDate DESC;
END
GO

-- Get ExamPeriods By AcademicYearID
CREATE PROCEDURE sp_ExamPeriod_GetByAcademicYearID
    @AcademicYearID INT
AS
BEGIN
    SELECT * FROM ExamPeriods WHERE AcademicYearID = @AcademicYearID ORDER BY StartDate DESC;
END
GO










-- Insert ExamType
CREATE PROCEDURE sp_ExamType_Insert
    @ExamTypeName NVARCHAR(50),
    @Description NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO ExamTypes (ExamTypeName, Description)
    VALUES (@ExamTypeName, @Description);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update ExamType
CREATE PROCEDURE sp_ExamType_Update
    @ExamTypeID INT,
    @ExamTypeName NVARCHAR(50),
    @Description NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE ExamTypes
    SET ExamTypeName = @ExamTypeName,
        Description = @Description
    WHERE ExamTypeID = @ExamTypeID;
END
GO

-- Delete ExamType
CREATE PROCEDURE sp_ExamType_Delete
    @ExamTypeID INT
AS
BEGIN
    DELETE FROM ExamTypes WHERE ExamTypeID = @ExamTypeID;
END
GO

-- Get ExamType By ID
CREATE PROCEDURE sp_ExamType_GetByID
    @ExamTypeID INT
AS
BEGIN
    SELECT * FROM ExamTypes WHERE ExamTypeID = @ExamTypeID;
END
GO

-- Get All ExamTypes
CREATE PROCEDURE sp_ExamType_GetAll
AS
BEGIN
    SELECT * FROM ExamTypes ORDER BY ExamTypeName;
END
GO






















-- Insert AcademicYear
CREATE PROCEDURE sp_AcademicYear_Insert
    @AcademicYearName NVARCHAR(50),
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    INSERT INTO AcademicYears (AcademicYearName, StartDate, EndDate)
    VALUES (@AcademicYearName, @StartDate, @EndDate);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update AcademicYear
CREATE PROCEDURE sp_AcademicYear_Update
    @AcademicYearID INT,
    @AcademicYearName NVARCHAR(50),
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    UPDATE AcademicYears
    SET AcademicYearName = @AcademicYearName,
        StartDate = @StartDate,
        EndDate = @EndDate
    WHERE AcademicYearID = @AcademicYearID;
END
GO

-- Delete AcademicYear
CREATE PROCEDURE sp_AcademicYear_Delete
    @AcademicYearID INT
AS
BEGIN
    DELETE FROM AcademicYears WHERE AcademicYearID = @AcademicYearID;
END
GO

-- Get AcademicYear By ID
CREATE PROCEDURE sp_AcademicYear_GetByID
    @AcademicYearID INT
AS
BEGIN
    SELECT * FROM AcademicYears WHERE AcademicYearID = @AcademicYearID;
END
GO

-- Get All AcademicYears
CREATE PROCEDURE sp_AcademicYear_GetAll
AS
BEGIN
    SELECT * FROM AcademicYears ORDER BY StartDate DESC;
END
GO



















-- Insert StudentStage
CREATE PROCEDURE sp_StudentStage_Insert
    @StudentID INT,
    @StageID INT,
    @AcademicYearID INT,
    @EnrollmentDate DATE,
    @IsActive BIT
AS
BEGIN
    INSERT INTO StudentStages (StudentID, StageID, AcademicYearID, EnrollmentDate, IsActive)
    VALUES (@StudentID, @StageID, @AcademicYearID, @EnrollmentDate, @IsActive);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update StudentStage
CREATE PROCEDURE sp_StudentStage_Update
    @StudentStageID INT,
    @StudentID INT,
    @StageID INT,
    @AcademicYearID INT,
    @EnrollmentDate DATE,
    @IsActive BIT
AS
BEGIN
    UPDATE StudentStages
    SET StudentID = @StudentID,
        StageID = @StageID,
        AcademicYearID = @AcademicYearID,
        EnrollmentDate = @EnrollmentDate,
        IsActive = @IsActive
    WHERE StudentStageID = @StudentStageID;
END
GO

-- Delete StudentStage
CREATE PROCEDURE sp_StudentStage_Delete
    @StudentStageID INT
AS
BEGIN
    DELETE FROM StudentStages WHERE StudentStageID = @StudentStageID;
END
GO

-- Get StudentStage By ID
CREATE PROCEDURE sp_StudentStage_GetByID
    @StudentStageID INT
AS
BEGIN
    SELECT * FROM StudentStages WHERE StudentStageID = @StudentStageID;
END
GO

-- Get All StudentStages
CREATE PROCEDURE sp_StudentStage_GetAll
AS
BEGIN
    SELECT * FROM StudentStages ORDER BY EnrollmentDate DESC;
END
GO

-- Get StudentStages By StudentID
CREATE PROCEDURE sp_StudentStage_GetByStudentID
    @StudentID INT
AS
BEGIN
    SELECT * FROM StudentStages WHERE StudentID = @StudentID ORDER BY EnrollmentDate DESC;
END
GO
















-- ÊÍÏíË ÍÇáÉ ÇáÊÝÚíá ÝÞØ
CREATE PROCEDURE sp_StudentStage_UpdateIsActive
    @StudentStageID INT,
    @IsActive BIT
AS
BEGIN
    UPDATE StudentStages
    SET IsActive = @IsActive
    WHERE StudentStageID = @StudentStageID;
END
GO

-- ÇáÈÍË ÍÓÈ StageID
CREATE PROCEDURE sp_StudentStage_GetByStageID
    @StageID INT
AS
BEGIN
    SELECT * FROM StudentStages WHERE StageID = @StageID ORDER BY EnrollmentDate DESC;
END
GO

-- ÇáÈÍË ÍÓÈ AcademicYearID
CREATE PROCEDURE sp_StudentStage_GetByAcademicYearID
    @AcademicYearID INT
AS
BEGIN
    SELECT * FROM StudentStages WHERE AcademicYearID = @AcademicYearID ORDER BY EnrollmentDate DESC;
END
GO






























-- 1. ÚÏÏ ÌãíÚ ÇáØáÇÈ ÇáãÓÌáíä (äÔØíä ÝÞØ)
CREATE PROCEDURE sp_GetStudentsEnrollmentCount
AS
BEGIN
    SELECT COUNT(*) AS EnrollmentCount FROM Students WHERE IsActive = 1;
END
GO

-- 2. ÞÇÆãÉ ÇáØáÇÈ ÇáãÓÌáíä (äÔØíä ÝÞØ) ãÚ ãÚáæãÇÊ ÃÓÇÓíÉ (PersonID + ÊÇÑíÎ ÇáÊÓÌíá + ÇáãÑÍáÉ)
CREATE PROCEDURE sp_GetStudentsEnrollment
AS
BEGIN
    SELECT 
        s.StudentID, s.PersonID, s.EnrollmentDate, s.StageID, s.IsActive,
        p.FirstName, p.SecondName, p.ThirdName, p.LastName
    FROM Students s
    INNER JOIN Persons p ON s.PersonID = p.PersonID
    WHERE s.IsActive = 1
    ORDER BY s.EnrollmentDate DESC;
END
GO

-- 3. ÚÏÏ ÇáØáÇÈ ÇáãÓÌáíä Ýí ãÑÍáÉ ãÚíäÉ (ãÚ ÈÇÑÇãíÊÑ ÇáãÑÍáÉ)
CREATE PROCEDURE sp_GetStudentsEnrollmentCountInStage
    @StageID INT
AS
BEGIN
    SELECT COUNT(*) AS EnrollmentCount FROM Students 
    WHERE IsActive = 1 AND StageID = @StageID;
END
GO

-- 4. ÞÇÆãÉ ÇáØáÇÈ ÇáãÓÌáíä Ýí ãÑÍáÉ ãÚíäÉ (äÔØíä ÝÞØ) ãÚ ÈíÇäÇÊåã ÇáÃÓÇÓíÉ
CREATE PROCEDURE sp_GetStudentsEnrollmentInStage
    @StageID INT
AS
BEGIN
    SELECT 
        s.StudentID, s.PersonID, s.EnrollmentDate, s.StageID, s.IsActive,
        p.FirstName, p.SecondName, p.ThirdName, p.LastName
    FROM Students s
    INNER JOIN Persons p ON s.PersonID = p.PersonID
    WHERE s.IsActive = 1 AND s.StageID = @StageID
    ORDER BY s.EnrollmentDate DESC;
END
GO




-- 1. ÚÏÏ ÇáØáÇÈ ÇáãÓÌáíä (äÔØíä) Öãä ÝÊÑÉ ÊÇÑíÎ ÊÓÌíá ãÚíäÉ
CREATE PROCEDURE sp_GetStudentsEnrollmentCountByDate
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT COUNT(*) AS EnrollmentCount 
    FROM Students
    WHERE IsActive = 1
      AND EnrollmentDate BETWEEN @StartDate AND @EndDate;
END
GO

-- 2. ÞÇÆãÉ ÇáØáÇÈ ÇáãÓÌáíä (äÔØíä) ãÚ ÈíÇäÇÊ ÃÓÇÓíÉ Öãä ÝÊÑÉ ÊÓÌíá ãÚíäÉ
CREATE PROCEDURE sp_GetStudentsEnrollmentByDate
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT 
        s.StudentID, s.PersonID, s.EnrollmentDate, s.StageID, s.IsActive,
        p.FirstName, p.SecondName, p.ThirdName, p.LastName
    FROM Students s
    INNER JOIN Persons p ON s.PersonID = p.PersonID
    WHERE s.IsActive = 1
      AND s.EnrollmentDate BETWEEN @StartDate AND @EndDate
    ORDER BY s.EnrollmentDate DESC;
END
GO

-- 3. ÚÏÏ ÇáØáÇÈ ÇáãÓÌáíä Ýí ãÑÍáÉ ãÚíäÉ Öãä ÝÊÑÉ ÊÓÌíá ãÚíäÉ
CREATE PROCEDURE sp_GetStudentsEnrollmentCountInStageByDate
    @StageID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT COUNT(*) AS EnrollmentCount
    FROM Students 
    WHERE IsActive = 1 
      AND StageID = @StageID
      AND EnrollmentDate BETWEEN @StartDate AND @EndDate;
END
GO

-- 4. ÞÇÆãÉ ÇáØáÇÈ ÇáãÓÌáíä Ýí ãÑÍáÉ ãÚíäÉ Öãä ÝÊÑÉ ÊÓÌíá ãÚíäÉ
CREATE PROCEDURE sp_GetStudentsEnrollmentInStageByDate
    @StageID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT 
        s.StudentID, s.PersonID, s.EnrollmentDate, s.StageID, s.IsActive,
        p.FirstName, p.SecondName, p.ThirdName, p.LastName
    FROM Students s
    INNER JOIN Persons p ON s.PersonID = p.PersonID
    WHERE s.IsActive = 1
      AND s.StageID = @StageID
      AND s.EnrollmentDate BETWEEN @StartDate AND @EndDate
    ORDER BY s.EnrollmentDate DESC;
END
GO




















create table EmployeeSalaryHistory
(
SalaryID INT PRIMARY KEY,
EmployeeID INT,
EffectiveDate DATE, -- ÊÇÑíÎ ÈÏÁ åÐÇ ÇáÑÇÊÈ
BaseSalary DECIMAL(18,2),
Allowances DECIMAL(18,2),
Deductions DECIMAL(18,2),
Notes NVARCHAR(255)
);



-- ÅÖÇÝÉ ÓÌá ÑÇÊÈ ÌÏíÏ
CREATE PROCEDURE sp_EmployeeSalaryHistory_Insert
    @EmployeeID INT,
    @EffectiveDate DATE,
    @BaseSalary DECIMAL(18,2),
    @Allowances DECIMAL(18,2),
    @Deductions DECIMAL(18,2),
    @Notes NVARCHAR(500)
AS
BEGIN
    INSERT INTO EmployeesSalaryHistory (EmployeeID, EffectiveDate, BaseSalary, Allowances, Deductions, Notes)
    VALUES (@EmployeeID, @EffectiveDate, @BaseSalary, @Allowances, @Deductions, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- ÊÍÏíË ÓÌá ÑÇÊÈ
CREATE PROCEDURE sp_EmployeeSalaryHistory_Update
    @SalaryID INT,
    @EmployeeID INT,
    @EffectiveDate DATE,
    @BaseSalary DECIMAL(18,2),
    @Allowances DECIMAL(18,2),
    @Deductions DECIMAL(18,2),
    @Notes NVARCHAR(500)
AS
BEGIN
    UPDATE EmployeesSalaryHistory SET
        EmployeeID = @EmployeeID,
        EffectiveDate = @EffectiveDate,
        BaseSalary = @BaseSalary,
        Allowances = @Allowances,
        Deductions = @Deductions,
        Notes = @Notes
    WHERE SalaryID = @SalaryID;
END
GO

-- ÍÐÝ ÓÌá ÑÇÊÈ
CREATE PROCEDURE sp_EmployeeSalaryHistory_Delete
    @SalaryID INT
AS
BEGIN
    DELETE FROM EmployeesSalaryHistory WHERE SalaryID = @SalaryID;
END
GO

-- ÇáÍÕæá Úáì ÓÌá ÈÑÞã SalaryID
CREATE PROCEDURE sp_EmployeeSalaryHistory_GetByID
    @SalaryID INT
AS
BEGIN
    SELECT * FROM EmployeesSalaryHistory WHERE SalaryID = @SalaryID;
END
GO

-- ÇáÍÕæá Úáì ßá ÇáÓÌáÇÊ
CREATE PROCEDURE sp_EmployeeSalaryHistory_GetAll
AS
BEGIN
    SELECT * FROM EmployeesSalaryHistory ORDER BY EffectiveDate DESC;
END
GO

-- ÇáÍÕæá Úáì ßá ÇáÑæÇÊÈ áãæÙÝ ãÍÏÏ
CREATE PROCEDURE sp_EmployeeSalaryHistory_GetByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM EmployeesSalaryHistory WHERE EmployeeID = @EmployeeID ORDER BY EffectiveDate DESC;
END
GO



















-- Insert Deduction
alter PROCEDURE sp_Deduction_Insert
    @EmployeeID INT,
    @Amount DECIMAL(18,2),
    @Reason NVARCHAR(255),
    @Date DATE,
    @AppliedInSalaryMonth DATE
AS
BEGIN
    INSERT INTO Deductions (EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth)
    VALUES (@EmployeeID, @Amount, @Reason, @Date, @AppliedInSalaryMonth);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Deduction
alter PROCEDURE sp_Deduction_Update
    @DeductionID INT,
    @EmployeeID INT,
    @Amount DECIMAL(18,2),
    @Reason NVARCHAR(255),
    @Date DATE,
    @AppliedInSalaryMonth DATE
AS
BEGIN
    UPDATE Deductions
    SET EmployeeID = @EmployeeID,
        Amount = @Amount,
        Reason = @Reason,
        Date = @Date,
        AppliedInSalaryMonth = @AppliedInSalaryMonth
    WHERE DeductionID = @DeductionID;
END
GO

-- Delete Deduction
alter PROCEDURE sp_Deduction_Delete
    @DeductionID INT
AS
BEGIN
    DELETE FROM Deductions WHERE DeductionID = @DeductionID;
END
GO

-- Get Deduction By ID
alter PROCEDURE sp_Deduction_GetByID
    @DeductionID INT
AS
BEGIN
    SELECT * FROM Deductions WHERE DeductionID = @DeductionID;
END
GO

-- Get All Deductions
alter PROCEDURE sp_Deduction_GetAll
AS
BEGIN
    SELECT * FROM Deductions ORDER BY Date DESC;
END
GO

-- Get Deductions By EmployeeID
alter PROCEDURE sp_Deduction_GetByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Deductions WHERE EmployeeID = @EmployeeID ORDER BY Date DESC;
END
GO

CREATE PROCEDURE sp_Bonus_GetTotalAmountByEmployeeAndPeriod
    @EmployeeID INT,
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT ISNULL(SUM(Amount), 0) AS TotalBonus
    FROM Bonuses
    WHERE EmployeeID = @EmployeeID
      AND Date >= @StartDate
      AND Date <= @EndDate;
END
GO



CREATE PROCEDURE sp_Bonus_GetByAppliedSalaryMonth
    @EmployeeID INT,
    @AppliedInSalaryMonth DATE
AS
BEGIN
    SELECT *
    FROM Bonuses
    WHERE EmployeeID = @EmployeeID
      AND AppliedInSalaryMonth = @AppliedInSalaryMonth
    ORDER BY Date DESC;
END
GO





-- Insert Bonus
alter PROCEDURE sp_Bonus_Insert
    @EmployeeID INT,
    @Amount DECIMAL(18,2),
    @Reason NVARCHAR(255),
    @Date DATE,
    @AppliedInSalaryMonth DATE
AS
BEGIN
    INSERT INTO Bonuses (EmployeeID, Amount, Reason, Date, AppliedInSalaryMonth)
    VALUES (@EmployeeID, @Amount, @Reason, @Date, @AppliedInSalaryMonth);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update Bonus
alter PROCEDURE sp_Bonus_Update
    @BonusID INT,
    @EmployeeID INT,
    @Amount DECIMAL(18,2),
    @Reason NVARCHAR(255),
    @Date DATE,
    @AppliedInSalaryMonth DATE
AS
BEGIN
    UPDATE Bonuses
    SET EmployeeID = @EmployeeID,
        Amount = @Amount,
        Reason = @Reason,
        Date = @Date,
        AppliedInSalaryMonth = @AppliedInSalaryMonth
    WHERE BonusID = @BonusID;
END
GO

-- Delete Bonus
alter PROCEDURE sp_Bonus_Delete
    @BonusID INT
AS
BEGIN
    DELETE FROM Bonuses WHERE BonusID = @BonusID;
END
GO

-- Get Bonus By ID
alter PROCEDURE sp_Bonus_GetByID
    @BonusID INT
AS
BEGIN
    SELECT * FROM Bonuses WHERE BonusID = @BonusID;
END
GO

-- Get All Bonuses
alter PROCEDURE sp_Bonus_GetAll
AS
BEGIN
    SELECT * FROM Bonuses ORDER BY Date DESC;
END
GO

-- Get Bonuses By EmployeeID
alter PROCEDURE sp_Bonus_GetByEmployeeID
    @EmployeeID INT
AS
BEGIN
    SELECT * FROM Bonuses WHERE EmployeeID = @EmployeeID ORDER BY Date DESC;
END
GO


























-- INSERT
CREATE PROCEDURE sp_StudentDiscipline_Insert
    @StudentID INT,
    @DisciplineDate DATE,
    @Points INT,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO StudentDiscipline (StudentID, DisciplineDate, Points, Notes)
    VALUES (@StudentID, @DisciplineDate, @Points, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- UPDATE
CREATE PROCEDURE sp_StudentDiscipline_Update
    @DisciplineID INT,
    @StudentID INT,
    @DisciplineDate DATE,
    @Points INT,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE StudentDiscipline
    SET StudentID = @StudentID,
        DisciplineDate = @DisciplineDate,
        Points = @Points,
        Notes = @Notes
    WHERE DisciplineID = @DisciplineID;
END
GO

-- DELETE
CREATE PROCEDURE sp_StudentDiscipline_Delete
    @DisciplineID INT
AS
BEGIN
    DELETE FROM StudentDiscipline WHERE DisciplineID = @DisciplineID;
END
GO

-- GET BY ID
CREATE PROCEDURE sp_StudentDiscipline_GetByID
    @DisciplineID INT
AS
BEGIN
    SELECT * FROM StudentDiscipline WHERE DisciplineID = @DisciplineID;
END
GO

-- GET ALL
CREATE PROCEDURE sp_StudentDiscipline_GetAll
AS
BEGIN
    SELECT * FROM StudentDiscipline;
END
GO






























-- Insert
alter PROCEDURE sp_StudentInstallment_Insert
    @StudentFeeID INT,
    @DueDate DATE,
    @Amount DECIMAL(18,2),
    @IsPaid BIT = NULL,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO StudentFeeInstallments (StudentFeeID, DueDate, Amount, IsPaid, Notes)
    VALUES (@StudentFeeID, @DueDate, @Amount, @IsPaid, @Notes);

    SELECT SCOPE_IDENTITY();
END
GO

-- Update
alter PROCEDURE sp_StudentInstallment_Update
    @InstallmentID INT,
    @StudentFeeID INT,
    @DueDate DATE,
    @Amount DECIMAL(18,2),
    @IsPaid BIT = NULL,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE StudentFeeInstallments
    SET StudentFeeID = @StudentFeeID,
        DueDate = @DueDate,
        Amount = @Amount,
        IsPaid = @IsPaid,
        Notes = @Notes
    WHERE InstallmentID = @InstallmentID;
END
GO

-- Delete
alter PROCEDURE sp_StudentInstallment_Delete
    @InstallmentID INT
AS
BEGIN
    DELETE FROM StudentFeeInstallments WHERE InstallmentID = @InstallmentID;
END
GO

-- Get By ID
alter PROCEDURE sp_StudentInstallment_GetByID
    @InstallmentID INT
AS
BEGIN
    SELECT * FROM StudentFeeInstallments WHERE InstallmentID = @InstallmentID;
END
GO

-- Get All
alter PROCEDURE sp_StudentInstallment_GetAll
AS
BEGIN
    SELECT * FROM StudentFeeInstallments;
END
GO


















-- Get Unpaid Installments (ÛíÑ ãÏÝæÚÉ)
alter PROCEDURE sp_StudentInstallment_GetUnpaid
AS
BEGIN
    SELECT * FROM StudentFeeInstallments WHERE IsPaid = 0 OR IsPaid IS NULL;
END
GO

-- Get Installments By StudentID
-- íÝÊÑÖ Ãä ÌÏæá StudentFee íÍÊæí Úáì ÚãæÏ StudentID
alter PROCEDURE sp_StudentInstallment_GetByStudentID
    @StudentID INT
AS
BEGIN
    SELECT I.*
    FROM StudentFeeInstallments I
    INNER JOIN StudentFees SF ON I.StudentFeeID = SF.StudentFeeID
    WHERE SF.StudentID = @StudentID;
END
GO

-- Get Installments By Date Range (ÍÓÈ DueDate)
alter PROCEDURE sp_StudentInstallment_GetByDateRange
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
    SELECT * FROM StudentFeeInstallments WHERE DueDate BETWEEN @StartDate AND @EndDate;
END
GO

-- Get Total Remaining Amount (ãÌãæÚ Amount ááÃÞÓÇØ ÛíÑ ÇáãÏÝæÚÉ)
alter PROCEDURE sp_StudentInstallment_GetTotalRemaining
AS
BEGIN
    SELECT ISNULL(SUM(Amount), 0) FROM StudentFeeInstallments WHERE IsPaid = 0 OR IsPaid IS NULL;
END
GO













CREATE PROCEDURE sp_FinalGrade_GetByStudentID
    @StudentID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT FinalGradeID, StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade, MaxGrade, CalculationDate, Notes
    FROM FinalGrades
    WHERE StudentID = @StudentID;
END


CREATE PROCEDURE sp_FinalGrade_GetBySubjectID
    @SubjectID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT FinalGradeID, StudentID, SubjectID, StageID, AcademicYearID, ExamPeriodID, ExamTypeID, Grade, MaxGrade, CalculationDate, Notes
    FROM FinalGrades
    WHERE SubjectID = @SubjectID;
END











alter PROCEDURE sp_Person_GetAllPaged
    @Offset INT,
    @Fetch INT,
    @Search NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Persons
    WHERE (@Search IS NULL OR FirstName LIKE '%' + @Search + '%' OR SecondName LIKE '%' + @Search + '%' OR LastName LIKE '%' + @Search + '%')
    ORDER BY PersonID
    OFFSET @Offset ROWS
    FETCH NEXT @Fetch ROWS ONLY;
END

