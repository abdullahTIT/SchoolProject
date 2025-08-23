create view vw_CurrentEmployeeSalary as
SELECT 
    E.EmployeeID, 
    CONCAT(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) AS FullName,
    S.BaseSalary, 
    S.Allowances, 
    S.Deductions, 
    S.EffectiveDate
FROM Employees E
INNER JOIN EmployeesSalaryHistory S ON E.EmployeeID = S.EmployeeID
INNER JOIN Persons P ON E.PersonID = P.PersonID
WHERE S.EffectiveDate = (
    SELECT MAX(EffectiveDate)
    FROM EmployeesSalaryHistory
    WHERE EmployeeID = E.EmployeeID
);












