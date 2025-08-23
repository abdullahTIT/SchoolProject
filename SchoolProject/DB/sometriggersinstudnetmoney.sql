CREATE TRIGGER trg_UpdateTotalPaid_AfterInsert
ON StudentPayments
AFTER INSERT
AS
BEGIN
    UPDATE sf
    SET sf.TotalPaid = sf.TotalPaid + i.AmountPaid
    FROM StudentFees sf
    INNER JOIN inserted i ON i.StudentFeeID = sf.StudentFeeID
END;

go






CREATE TRIGGER trg_PreventOverPayment
ON StudentPayments
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted i
        JOIN StudentFees sf ON i.StudentFeeID = sf.StudentFeeID
        WHERE i.AmountPaid + sf.TotalPaid > 
              (SELECT Amount FROM StageFeeItems WHERE StageFeeItemID = sf.StageFeeItemID)
    )
    BEGIN
        RAISERROR('Amount exceeds the total required fee.', 16, 1);
        ROLLBACK;
        RETURN;
    END

    -- ÅÐÇ áã ÊÊÌÇæÒ ÇáãÈáÛ ÇáãØáæÈ¡ Þã ÈÇáÅÏÎÇá ÇáÝÚáí
    INSERT INTO StudentPayments (StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes)
    SELECT StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes
    FROM inserted;
END;


go






CREATE TRIGGER trg_UpdateTotalPaid_AfterDelete
ON StudentPayments
AFTER DELETE
AS
BEGIN
    UPDATE sf
    SET sf.TotalPaid = sf.TotalPaid - d.AmountPaid
    FROM StudentFees sf
    INNER JOIN deleted d ON d.StudentFeeID = sf.StudentFeeID
END;

go







--CREATE VIEW vw_StudentFeeStatus AS
--SELECT 
--    sf.StudentFeeID,
--    sf.StudentID,
--    ft.FeeName,
--    sfi.Amount AS TotalRequired,
--    sf.TotalPaid,
--    (sfi.Amount - sf.TotalPaid) AS Remaining,
--    CASE 
--        WHEN sf.TotalPaid >= sfi.Amount THEN 'Paid'
--        WHEN sf.TotalPaid = 0 THEN 'Unpaid'
--        ELSE 'Partial'
--    END AS PaymentStatus,
--    sf.DueDate,
--    sf.Status,
--    sf.Notes
--FROM StudentFees sf
--JOIN StageFeeItems sfi ON sf.StageFeeItemID = sfi.StageFeeItemID
--JOIN FeeTypes ft ON sfi.FeeTypeID = ft.FeeTypeID,

--go

CREATE OR ALTER VIEW vw_StudentFeeStatus AS
SELECT 
    sf.StudentFeeID,
    sf.StudentID,
 RTRIM(LTRIM(
        ISNULL(p.FirstName, '') + ' ' +
        ISNULL(p.SecondName, '') + ' ' +
        ISNULL(p.ThirdName, '') + ' ' +
        ISNULL(p.LastName, '')
    )) AS FullName,    ft.FeeName,
    sfi.Amount AS TotalRequired,
    sf.TotalPaid,
    (sfi.Amount - sf.TotalPaid) AS Remaining,
    CASE 
        WHEN sf.TotalPaid >= sfi.Amount THEN 'Paid'
        WHEN sf.TotalPaid = 0 THEN 'Unpaid'
        ELSE 'Partial'
    END AS PaymentStatus,
    sf.DueDate,
    sf.Status,
    sf.Notes
FROM StudentFees sf
JOIN StageFeeItems sfi ON sf.StageFeeItemID = sfi.StageFeeItemID
JOIN FeeTypes ft ON sfi.FeeTypeID = ft.FeeTypeID
JOIN Students st ON sf.StudentID = st.StudentID
JOIN Persons p ON st.PersonID = p.PersonID;
GO




select * from Students



ALTER TABLE StudentPayments
ADD CONSTRAINT CHK_AmountPaid_Positive
CHECK (AmountPaid >= 0);










CREATE NONCLUSTERED INDEX idx_StudentFee_StudentID
ON StudentFees(StudentID);

CREATE NONCLUSTERED INDEX idx_StudentPayments_StudentFeeID
ON StudentPayments(StudentFeeID);









CREATE TRIGGER trg_AutoAddFees_ForNewStudent
ON Students
AFTER INSERT
AS
BEGIN
    INSERT INTO StudentFees (StudentID, StageFeeItemID, DueDate, Status, Notes, TotalPaid)
    SELECT 
        i.StudentID,
        sfi.StageFeeItemID,
        DATEADD(DAY, 30, i.EnrollmentDate),  -- ÈÚÏ 30 íæã ãä ÇáÊÓÌíá
        'Pending',
        'Auto-generated based on stage',
        0.00
    FROM inserted i
    JOIN StageFeeItems sfi ON sfi.StageID = i.StageID
    WHERE sfi.IsActive = 1;
END;








go
CREATE TABLE StudentFeeInstallments (
    InstallmentID INT IDENTITY PRIMARY KEY,
    StudentFeeID INT NOT NULL,
    DueDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    IsPaid BIT DEFAULT 0,
    Notes NVARCHAR(255),
    CONSTRAINT FK_Installments_StudentFee FOREIGN KEY (StudentFeeID) REFERENCES StudentFees(StudentFeeID)
);

go








CREATE OR ALTER VIEW vw_UpcomingInstallments AS
SELECT 
    i.InstallmentID,
    i.StudentFeeID,
    sf.StudentID,
    -- ÌãÚ ÃÓãÇÁ ÇáØÇáÈ ãÚ ãÓÇÝÇÊ Èíäåã
    RTRIM(LTRIM(
        ISNULL(p.FirstName, '') + ' ' +
        ISNULL(p.SecondName, '') + ' ' +
        ISNULL(p.ThirdName, '') + ' ' +
        ISNULL(p.LastName, '')
    )) AS FullName,
    i.DueDate,
    i.Amount,
    i.IsPaid
FROM StudentFeeInstallments i
JOIN StudentFees sf ON sf.StudentFeeID = i.StudentFeeID
JOIN Students s ON s.StudentID = sf.StudentID
JOIN Persons p ON p.PersonID = s.PersonID
WHERE i.IsPaid = 0 
  AND i.DueDate > GETDATE();
GO



CREATE TRIGGER trg_UpdateInstallmentStatus
ON StudentPayments
AFTER INSERT
AS
BEGIN
    UPDATE i
    SET i.IsPaid = 1
    FROM StudentFeeInstallments i
    JOIN StudentFees sf ON i.StudentFeeID = sf.StudentFeeID
    JOIN inserted p ON p.StudentFeeID = sf.StudentFeeID
    WHERE i.IsPaid = 0 AND p.AmountPaid >= i.Amount
      AND i.DueDate <= p.PaymentDate;
END;
go




go


CREATE OR ALTER TRIGGER trg_UpdateFeeStatusBasedOnInstallments
ON StudentFeeInstallments
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- ÊÍÏíË ÍÇáÉ ÇáÑÓæã Åáì 'Late' ÅÐÇ Ýíå ÞÓØ ãÊÃÎÑ (ÛíÑ ãÏÝæÚ æÊÇÑíÎ ÇÓÊÍÞÇÞå ÝÇÊ)
    UPDATE sf
    SET sf.Status = 'Late'
    FROM StudentFees sf
    WHERE EXISTS (
        SELECT 1 FROM StudentFeeInstallments i
        WHERE i.StudentFeeID = sf.StudentFeeID
          AND i.IsPaid = 0
          AND i.DueDate < GETDATE()
    );

    -- ÊÍÏíË ÍÇáÉ ÇáÑÓæã Åáì 'Active' ÅÐÇ ÌãíÚ ÇáÃÞÓÇØ ãÏÝæÚÉ
    UPDATE sf
    SET sf.Status = 'Active'
    FROM StudentFees sf
    WHERE NOT EXISTS (
        SELECT 1 FROM StudentFeeInstallments i
        WHERE i.StudentFeeID = sf.StudentFeeID
          AND i.IsPaid = 0
    );
END;
GO







CREATE VIEW vw_StudentFinancialStatus AS
SELECT 
    s.StudentID,
    sf.StudentFeeID,
    ft.FeeName,
    sfi.Amount AS TotalFee,
    sf.TotalPaid,
    (sfi.Amount - sf.TotalPaid) AS Remaining,
    COUNT(i.InstallmentID) AS TotalInstallments,
    SUM(CASE WHEN i.IsPaid = 1 THEN 1 ELSE 0 END) AS PaidInstallments,
    SUM(CASE WHEN i.IsPaid = 0 THEN 1 ELSE 0 END) AS UnpaidInstallments,
    MAX(i.DueDate) AS LastDueDate,
    CASE 
        WHEN sfi.Amount - sf.TotalPaid <= 0 THEN 'Paid'
        WHEN MAX(i.DueDate) < GETDATE() THEN 'Overdue'
        ELSE 'Partial'
    END AS PaymentStatus
FROM Students s
JOIN StudentFees sf ON s.StudentID = sf.StudentID
JOIN StageFeeItems sfi ON sf.StageFeeItemID = sfi.StageFeeItemID
JOIN FeeTypes ft ON sfi.FeeTypeID = ft.FeeTypeID
LEFT JOIN StudentFeeInstallments i ON i.StudentFeeID = sf.StudentFeeID
GROUP BY s.StudentID, sf.StudentFeeID, ft.FeeName, sfi.Amount, sf.TotalPaid;


go




UPDATE sf
SET sf.Status = 'Late'
FROM StudentFees sf
JOIN StageFeeItems sfi ON sf.StageFeeItemID = sfi.StageFeeItemID
WHERE EXISTS (
    SELECT 1
    FROM StudentFeeInstallments i
    WHERE i.StudentFeeID = sf.StudentFeeID
      AND i.IsPaid = 0
      AND i.DueDate < GETDATE()
);



-- 2. ÅäÔÇÁ Job ãä SQL Server Agent
--ÇÝÊÍ SQL Server Management Studio¡ Ëã:

--ÇÐåÈ Åáì: SQL Server Agent ? Jobs ? New Job

--ÇáÇÓã: Update Late StudentFees Weekly

--Ýí ÊÈæíÈ Steps:

--Step Name: UpdateLateFees

--Type: Transact-SQL

--Command: ÇáÕíÛÉ ÃÚáÇå

--Ýí ÊÈæíÈ Schedules:

--Schedule Name: Weekly Late Update

--Frequency: Weekly

--Recurs every: 1 week

--Occurs on: Saturday (Ãæ Çáíæã ÇáÐí ÊÑíÏå)

--Time: ãËáðÇ 2:00 AM





go




