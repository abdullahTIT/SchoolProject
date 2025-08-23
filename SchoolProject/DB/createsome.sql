-- ≈‰‘«¡ ÃœÊ· «·Œ’Ê„« 
CREATE TABLE Deductions (
    DeductionID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Reason NVARCHAR(255),
    Date DATE NOT NULL,
    AppliedInSalaryMonth DATE NOT NULL
);

-- ≈‰‘«¡ ÃœÊ· «·„ﬂ«›¬ 
CREATE TABLE Bonuses (
    BonusID INT PRIMARY KEY IDENTITY(1,1),
    EmployeeID INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Reason NVARCHAR(255),
    Date DATE NOT NULL,
    AppliedInSalaryMonth DATE NOT NULL
);
