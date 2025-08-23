CREATE VIEW vw_EmployeeMonthlySalary AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    s.SalaryMonth,
    s.BaseSalary,
    s.AbsenceDays,
    s.DeductionPerAbsent,
    (s.BaseSalary - (s.AbsenceDays * s.DeductionPerAbsent)) AS NetSalary,
    s.Paid,
    s.PaymentDate
FROM MonthlySalaries s
JOIN Employees e ON s.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID;
GO

CREATE VIEW vw_EmployeeMonthlyAttendance AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    FORMAT(a.AttendanceDate, 'yyyy-MM') AS SalaryMonth,
    COUNT(*) AS PresentDays
FROM EmployeeAttendance a
JOIN Employees e ON a.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID
WHERE a.Status = 'Present'
GROUP BY e.EmployeeID, p.FirstName, p.SecondName, p.ThirdName, p.LastName, FORMAT(a.AttendanceDate, 'yyyy-MM');
GO

CREATE VIEW vw_FullEmployeeMonthlySalaryDetails AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    s.SalaryMonth,
    s.BaseSalary,
    s.AbsenceDays,
    s.DeductionPerAbsent,
    (s.AbsenceDays * s.DeductionPerAbsent) AS TotalDeduction,
    (s.BaseSalary - (s.AbsenceDays * s.DeductionPerAbsent)) AS NetSalary,
    s.Paid,
    s.PaymentDate
FROM MonthlySalaries s
JOIN Employees e ON s.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID;
GO

CREATE VIEW vw_UnpaidEmployees AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    s.SalaryMonth,
    s.BaseSalary,
    s.NetSalary,
    s.Paid
FROM MonthlySalaries s
JOIN Employees e ON s.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID
WHERE s.Paid = 0;
GO

CREATE VIEW vw_EmployeeMonthlyAbsence AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    FORMAT(a.AttendanceDate, 'yyyy-MM') AS SalaryMonth,
    COUNT(*) AS AbsenceDays
FROM EmployeeAttendance a
JOIN Employees e ON a.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID
WHERE a.Status = 'Absent'
GROUP BY e.EmployeeID, p.FirstName, p.SecondName, p.ThirdName, p.LastName, FORMAT(a.AttendanceDate, 'yyyy-MM');
GO

CREATE VIEW vw_TerminatedEmployees AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    e.JobTitleID,
    e.HireDate,
    e.TerminationDate,
    e.Notes
FROM Employees e
JOIN Persons p ON e.PersonID = p.PersonID
WHERE e.TerminationDate IS NOT NULL;
GO

CREATE VIEW vw_EmployeeAnnualSalarySummary AS
SELECT
    e.EmployeeID,
    p.FirstName + ' ' + p.SecondName + ' ' + p.ThirdName + ' ' + p.LastName AS FullName,
    YEAR(s.SalaryMonth) AS SalaryYear,
    SUM(s.BaseSalary) AS TotalBaseSalary,
    SUM(s.AbsenceDays * s.DeductionPerAbsent) AS TotalDeductions,
    SUM(s.NetSalary) AS TotalNetSalary
FROM MonthlySalaries s
JOIN Employees e ON s.EmployeeID = e.EmployeeID
JOIN Persons p ON e.PersonID = p.PersonID
WHERE s.Paid = 1
GROUP BY e.EmployeeID, p.FirstName, p.SecondName, p.ThirdName, p.LastName, YEAR(s.SalaryMonth);
GO











CREATE TABLE StudentDiscipline (
    DisciplineID INT IDENTITY(1,1) PRIMARY KEY,
    StudentID INT NOT NULL,
    DisciplineDate DATE NOT NULL,
    Points INT NOT NULL default 100,                     
    Notes NVARCHAR(255) NULL
);
