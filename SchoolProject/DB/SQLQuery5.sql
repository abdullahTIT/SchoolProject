USE [SchoolDB]
GO

INSERT INTO [dbo].[Employees]
           ([PersonID]
           ,[JobTitleID]
           ,[HireDate]
           ,[TerminationDate]
           ,[IsActive]
           ,[Notes])
     VALUES
           (<PersonID, int,>
           ,<JobTitleID, int,>
           ,<HireDate, date,>
           ,<TerminationDate, date,>
           ,<IsActive, bit,>
           ,<Notes, nvarchar(500),>)
GO

CREATE TABLE Attendance (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    WorkDate DATE NOT NULL,
    Status NVARCHAR(20) NOT NULL CHECK (Status IN ('Present', 'Absent', 'Late')),
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);

select * from Employees

UPDATE Employees
SET Salary = ms.BaseSalary
FROM Employees e
JOIN MonthlySalaries ms ON e.EmployeeID = ms.EmployeeID
WHERE ms.SalaryMonth = '2025-06-01';

INSERT INTO Attendance (EmployeeID, WorkDate, Status) VALUES
(1, '2025-06-01', 'Present'),
(2, '2025-06-01', 'Absent'),
(3, '2025-06-01', 'Late'),
(4, '2025-06-01', 'Present'),
(5, '2025-06-01', 'Present'),

(1, '2025-06-02', 'Late'),
(2, '2025-06-02', 'Present'),
(3, '2025-06-02', 'Present'),
(4, '2025-06-02', 'Absent'),
(5, '2025-06-02', 'Present'),

(6, '2025-06-01', 'Present'),
(7, '2025-06-01', 'Late'),
(8, '2025-06-01', 'Absent'),
(9, '2025-06-01', 'Present'),
(10, '2025-06-01', 'Present'),

(6, '2025-06-02', 'Absent'),
(7, '2025-06-02', 'Present'),
(8, '2025-06-02', 'Late'),
(9, '2025-06-02', 'Present'),
(10, '2025-06-02', 'Absent'),

-- »Ì«‰«  ·⁄œœ √ﬂ»— „‰ «·„ÊŸ›Ì‰
(11, '2025-06-01', 'Present'),
(12, '2025-06-01', 'Late'),
(13, '2025-06-01', 'Present'),
(14, '2025-06-01', 'Absent'),
(15, '2025-06-01', 'Present'),

(16, '2025-06-01', 'Late'),
(17, '2025-06-01', 'Present'),
(18, '2025-06-01', 'Absent'),
(19, '2025-06-01', 'Present'),
(20, '2025-06-01', 'Late'),

(21, '2025-06-01', 'Present'),
(22, '2025-06-01', 'Present'),
(23, '2025-06-01', 'Absent'),
(24, '2025-06-01', 'Late'),
(25, '2025-06-01', 'Present'),

(26, '2025-06-01', 'Absent'),
(27, '2025-06-01', 'Present'),
(28, '2025-06-01', 'Late'),
(29, '2025-06-01', 'Present'),
(30, '2025-06-01', 'Present');


INSERT INTO MonthlySalaries (EmployeeID, SalaryMonth, BaseSalary, AbsenceDays, DeductionPerAbsent, Paid, PaymentDate) VALUES
(1, '2025-06-01', 3000.00, 0, 100.00, 1, '2025-06-28'),
(2, '2025-06-01', 2800.00, 2, 90.00, 1, '2025-06-29'),
(3, '2025-06-01', 3200.00, 1, 110.00, 0, NULL),
(4, '2025-06-01', 3100.00, 0, 95.00, 1, '2025-06-27'),
(5, '2025-06-01', 2900.00, 3, 85.00, 0, NULL),

(6, '2025-06-01', 3300.00, 0, 120.00, 1, '2025-06-28'),
(7, '2025-06-01', 3050.00, 1, 100.00, 1, '2025-06-30'),
(8, '2025-06-01', 2950.00, 0, 90.00, 1, '2025-06-29'),
(9, '2025-06-01', 3100.00, 2, 95.00, 0, NULL),
(10, '2025-06-01', 3000.00, 1, 100.00, 1, '2025-06-28'),

(11, '2025-06-01', 2800.00, 0, 85.00, 1, '2025-06-29'),
(12, '2025-06-01', 3200.00, 0, 110.00, 1, '2025-06-27'),
(13, '2025-06-01', 2900.00, 4, 90.00, 0, NULL),
(14, '2025-06-01', 3000.00, 0, 100.00, 1, '2025-06-30'),
(15, '2025-06-01', 3100.00, 1, 95.00, 1, '2025-06-29'),

(16, '2025-06-01', 2950.00, 2, 90.00, 0, NULL),
(17, '2025-06-01', 3050.00, 0, 100.00, 1, '2025-06-28'),
(18, '2025-06-01', 3000.00, 0, 100.00, 1, '2025-06-27'),
(19, '2025-06-01', 3100.00, 3, 95.00, 0, NULL),
(20, '2025-06-01', 3200.00, 0, 110.00, 1, '2025-06-30'),

(21, '2025-06-01', 2800.00, 1, 85.00, 1, '2025-06-29'),
(22, '2025-06-01', 3000.00, 0, 100.00, 1, '2025-06-28'),
(23, '2025-06-01', 3100.00, 2, 95.00, 0, NULL),
(24, '2025-06-01', 2900.00, 0, 90.00, 1, '2025-06-27'),
(25, '2025-06-01', 3050.00, 1, 100.00, 1, '2025-06-29'),

(26, '2025-06-01', 2950.00, 0, 90.00, 1, '2025-06-30'),
(27, '2025-06-01', 3000.00, 3, 100.00, 0, NULL),
(28, '2025-06-01', 3200.00, 0, 110.00, 1, '2025-06-28'),
(29, '2025-06-01', 3100.00, 0, 95.00, 1, '2025-06-29'),
(30, '2025-06-01', 2800.00, 2, 85.00, 0, NULL);


CREATE TABLE MonthlySalaries (
    SalaryID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    SalaryMonth DATE NOT NULL,
    BaseSalary DECIMAL(18,2) NOT NULL CHECK (BaseSalary >= 0),
    AbsenceDays INT NOT NULL DEFAULT 0 CHECK (AbsenceDays >= 0),
    DeductionPerAbsent DECIMAL(18,2) NOT NULL DEFAULT 0 CHECK (DeductionPerAbsent >= 0),
    -- ‰Õ–› ⁄„Êœ TotalDeductions
    NetSalary AS (BaseSalary - (AbsenceDays * DeductionPerAbsent)),
    Paid BIT NOT NULL DEFAULT 0,
    PaymentDate DATE NULL,
    FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID)
);



CREATE TABLE EmployeeAttendance (
    AttendanceID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    AttendanceDate DATE NOT NULL,
    CheckInTime TIME NULL,
    CheckOutTime TIME NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN ('Present', 'Absent', 'Sick', 'Leave')),

    CONSTRAINT FK_Attendance_Employee FOREIGN KEY (EmployeeID) REFERENCES Employees(EmployeeID),
    CONSTRAINT UQ_Attendance_UniqueDay UNIQUE(EmployeeID, AttendanceDate)
);


-- Õ–› »Ì«‰«  ”«»ﬁ… · Ã‰» «· ⁄«—÷ («Œ Ì«—Ì)
DELETE FROM EmployeeAttendance WHERE AttendanceDate BETWEEN '2025-06-01' AND '2025-06-07';

-- ≈œŒ«· »Ì«‰«  ÊÂ„Ì… ·„œ… 7 √Ì«„ ··„ÊŸ›Ì‰ 1 ≈·Ï 30


