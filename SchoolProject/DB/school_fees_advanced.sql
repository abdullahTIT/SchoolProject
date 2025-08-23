
-- ===============================
-- 1. جدول StudentFeeInstallments (الأقساط)
-- ===============================
CREATE TABLE StudentFeeInstallments (
    InstallmentID INT IDENTITY PRIMARY KEY,
    StudentFeeID INT NOT NULL,
    DueDate DATE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    IsPaid BIT DEFAULT 0,
    Notes NVARCHAR(255),
    CONSTRAINT FK_Installments_StudentFee FOREIGN KEY (StudentFeeID) REFERENCES StudentFees(StudentFeeID)
);
GO

-- ===============================
-- 2. جدول StudentPayments_Audit (تتبع التعديلات)
-- ===============================
CREATE TABLE StudentPayments_Audit (
    AuditID INT IDENTITY PRIMARY KEY,
    PaymentID INT,
    StudentFeeID INT,
    PaymentDate DATE,
    AmountPaid DECIMAL(18,2),
    PaymentMethod NVARCHAR(50),
    Notes NVARCHAR(255),
    ActionType NVARCHAR(10), -- INSERT / UPDATE / DELETE
    ActionDate DATETIME DEFAULT GETDATE(),
    PerformedBy NVARCHAR(100) DEFAULT SYSTEM_USER
);
GO

-- ===============================
-- 3. Trigger لتحديث TotalPaid بعد INSERT على StudentPayments
-- ===============================
CREATE TRIGGER trg_UpdateTotalPaid_AfterInsert
ON StudentPayments
AFTER INSERT
AS
BEGIN
    UPDATE sf
    SET sf.TotalPaid = sf.TotalPaid + i.AmountPaid
    FROM StudentFees sf
    INNER JOIN inserted i ON i.StudentFeeID = sf.StudentFeeID;
END;
GO

-- ===============================
-- 4. Trigger لمنع الدفع أكثر من الرسوم المطلوبة
-- ===============================
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

    INSERT INTO StudentPayments (StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes)
    SELECT StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes
    FROM inserted;
END;
GO

-- ===============================
-- 5. Trigger لتحديث TotalPaid بعد DELETE من StudentPayments
-- ===============================
CREATE TRIGGER trg_UpdateTotalPaid_AfterDelete
ON StudentPayments
AFTER DELETE
AS
BEGIN
    UPDATE sf
    SET sf.TotalPaid = sf.TotalPaid - d.AmountPaid
    FROM StudentFees sf
    INNER JOIN deleted d ON d.StudentFeeID = sf.StudentFeeID;
END;
GO

-- ===============================
-- 6. Trigger تلقائي لإضافة رسوم الطالب عند تسجيله في Students
-- ===============================
CREATE TRIGGER trg_AutoAddFees_ForNewStudent
ON Students
AFTER INSERT
AS
BEGIN
    INSERT INTO StudentFees (StudentID, StageFeeItemID, DueDate, Status, Notes, TotalPaid)
    SELECT 
        i.StudentID,
        sfi.StageFeeItemID,
        DATEADD(DAY, 30, i.EnrollmentDate),  
        'Pending',
        'Auto-generated based on stage',
        0.00
    FROM inserted i
    JOIN StageFeeItems sfi ON sfi.StageID = i.StageID
    WHERE sfi.IsActive = 1;
END;
GO

-- ===============================
-- 7. Trigger لتحديث حالة القسط عند دفعه (StudentPayments → StudentFeeInstallments)
-- ===============================
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
GO

-- ===============================
-- 8. Trigger لتحديث حالة الرسوم المتأخرة بناء على الأقساط
-- ===============================
CREATE TRIGGER trg_UpdateLateStatus
ON StudentFeeInstallments
AFTER UPDATE
AS
BEGIN
    UPDATE sf
    SET sf.Status = 'Late'
    FROM StudentFees sf
    JOIN StudentFeeInstallments i ON sf.StudentFeeID = i.StudentFeeID
    WHERE i.IsPaid = 0 AND i.DueDate < GETDATE();
END;
GO

-- ===============================
-- 9. View لعرض حالة الطالب المالية والأقساط
-- ===============================
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
GO

-- ===============================
-- 10. View للإجمالي المحصل لكل سنة دراسية ومرحلة
-- ===============================
CREATE VIEW vw_CollectedFeesSummary AS
SELECT 
    ay.AcademicYearID,
    ay.AcademicYearName,
    st.StageID,
    st.StageName,
    SUM(sfi.Amount) AS TotalRequired,
    SUM(sf.TotalPaid) AS TotalCollected,
    SUM(sfi.Amount) - SUM(sf.TotalPaid) AS TotalRemaining
FROM StudentFees sf
JOIN StageFeeItems sfi ON sf.StageFeeItemID = sfi.StageFeeItemID
JOIN AcademicYears ay ON sfi.AcademicYearID = ay.AcademicYearID
JOIN Stages st ON sfi.StageID = st.StageID
GROUP BY ay.AcademicYearID, ay.AcademicYearName, st.StageID, st.StageName;
GO

-- ===============================
-- 11. Constraint لمنع دفع مبلغ سلبي
-- ===============================
ALTER TABLE StudentPayments
ADD CONSTRAINT CHK_AmountPaid_Positive
CHECK (AmountPaid >= 0);
GO

-- ===============================
-- 12. Indexes لتحسين الأداء
-- ===============================
CREATE NONCLUSTERED INDEX idx_StudentFee_StudentID
ON StudentFees(StudentID);
GO

CREATE NONCLUSTERED INDEX idx_StudentPayments_StudentFeeID
ON StudentPayments(StudentFeeID);
GO

-- ===============================
-- 13. Triggers لإضافة بيانات في جدول Audit عند INSERT, UPDATE, DELETE في StudentPayments
-- ===============================
CREATE TRIGGER trg_StudentPayments_InsertAudit
ON StudentPayments
AFTER INSERT
AS
BEGIN
    INSERT INTO StudentPayments_Audit (PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, ActionType)
    SELECT PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, 'INSERT'
    FROM inserted;
END;
GO

CREATE TRIGGER trg_StudentPayments_UpdateAudit
ON StudentPayments
AFTER UPDATE
AS
BEGIN
    INSERT INTO StudentPayments_Audit (PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, ActionType)
    SELECT PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, 'UPDATE'
    FROM inserted;
END;
GO

CREATE TRIGGER trg_StudentPayments_DeleteAudit
ON StudentPayments
AFTER DELETE
AS
BEGIN
    INSERT INTO StudentPayments_Audit (PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, ActionType)
    SELECT PaymentID, StudentFeeID, PaymentDate, AmountPaid, PaymentMethod, Notes, 'DELETE'
    FROM deleted;
END;
GO
