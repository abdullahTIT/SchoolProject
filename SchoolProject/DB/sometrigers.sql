CREATE TRIGGER trg_CalculateAbsence
ON EmployeeAttendance
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE MS
    SET MS.AbsenceDays = (
        SELECT COUNT(*)
        FROM EmployeeAttendance EA
        WHERE EA.EmployeeID = MS.EmployeeID AND EA.Status = 'Absent'
            AND FORMAT(EA.AttendanceDate, 'yyyy-MM') = FORMAT(MS.SalaryMonth, 'yyyy-MM')
    )
    FROM MonthlySalaries MS
    INNER JOIN inserted i ON i.EmployeeID = MS.EmployeeID;
END




CREATE TRIGGER trg_UpdateNetSalary
ON MonthlySalaries
AFTER INSERT, UPDATE
AS
BEGIN
    UPDATE MonthlySalaries
    SET NetSalary = 
        (BaseSalary - (AbsenceDays * DeductionPerAbsent))
    WHERE SalaryID IN (SELECT SalaryID FROM inserted);
END

go



CREATE TRIGGER trg_BonusAffectsSalary
ON Bonuses
AFTER INSERT
AS
BEGIN
    UPDATE MS
    SET NetSalary = NetSalary + i.Amount
    FROM MonthlySalaries MS
    INNER JOIN inserted i ON i.EmployeeID = MS.EmployeeID
    WHERE FORMAT(MS.SalaryMonth, 'yyyy-MM') = FORMAT(i.AppliedInSalaryMonth, 'yyyy-MM');
END


go


CREATE TRIGGER trg_DeductionAffectsSalary
ON Deductions
AFTER INSERT
AS
BEGIN
    UPDATE MS
    SET NetSalary = NetSalary - i.Amount
    FROM MonthlySalaries MS
    INNER JOIN inserted i ON i.EmployeeID = MS.EmployeeID
    WHERE FORMAT(MS.SalaryMonth, 'yyyy-MM') = FORMAT(i.AppliedInSalaryMonth, 'yyyy-MM');
END

go