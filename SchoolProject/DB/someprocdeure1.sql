-- 1. ≈œŒ«· ”Ã· ÃœÌœ
CREATE PROCEDURE Insert_IdentifierType
    @IdentifierName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        INSERT INTO IdentifierTypes (IdentifierName)
        VALUES (@IdentifierName);
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 2.  ÕœÌÀ ”Ã· „ÊÃÊœ
CREATE PROCEDURE Update_IdentifierType
    @IdentifierTypeID INT,
    @IdentifierName NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM IdentifierTypes WHERE IdentifierTypeID = @IdentifierTypeID)
        BEGIN
            UPDATE IdentifierTypes
            SET IdentifierName = @IdentifierName
            WHERE IdentifierTypeID = @IdentifierTypeID;
        END
        ELSE
        BEGIN
            RAISERROR('«·”Ã· €Ì— „ÊÃÊœ ·· ÕœÌÀ.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 3. Õ–› ”Ã· „ÊÃÊœ
CREATE PROCEDURE Delete_IdentifierType
    @IdentifierTypeID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM IdentifierTypes WHERE IdentifierTypeID = @IdentifierTypeID)
        BEGIN
            DELETE FROM IdentifierTypes
            WHERE IdentifierTypeID = @IdentifierTypeID;
        END
        ELSE
        BEGIN
            RAISERROR('«·”Ã· €Ì— „ÊÃÊœ ··Õ–›.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

-- 4. Ã·» ﬂ· «·”Ã·« 
CREATE PROCEDURE Get_All_IdentifierTypes
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT IdentifierTypeID, IdentifierName
        FROM IdentifierTypes
        ORDER BY IdentifierTypeID;
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

CREATE PROCEDURE Get_IdentifierType_ByID
    @IdentifierTypeID INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF EXISTS (SELECT 1 FROM IdentifierTypes WHERE IdentifierTypeID = @IdentifierTypeID)
        BEGIN
            SELECT IdentifierTypeID, IdentifierName
            FROM IdentifierTypes
            WHERE IdentifierTypeID = @IdentifierTypeID;
        END
        ELSE
        BEGIN
            RAISERROR('«·”Ã· €Ì— „ÊÃÊœ.', 16, 1);
        END
    END TRY
    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO


-- ≈÷«›… ”Ã· ÃœÌœ
CREATE PROCEDURE Insert_Identifier
    @IdentifierTypeID INT,
    @IdentifierValue NVARCHAR(100)
AS
BEGIN
    INSERT INTO Identifiers (IdentifierTypeID, IdentifierValue)
    VALUES (@IdentifierTypeID, @IdentifierValue);

    SELECT SCOPE_IDENTITY() AS NewIdentifierID;
END
GO

-- «” ⁄·«„ ﬂ· «·”Ã·« 
CREATE PROCEDURE Get_All_Identifiers
AS
BEGIN
    SELECT IdentifierID, IdentifierTypeID, IdentifierValue
    FROM Identifiers;
END
GO

-- «” ⁄·«„ ”Ã· Õ”» «·„⁄—›
CREATE PROCEDURE Get_IdentifierByID
    @IdentifierID INT
AS
BEGIN
    SELECT IdentifierID, IdentifierTypeID, IdentifierValue
    FROM Identifiers
    WHERE IdentifierID = @IdentifierID;
END
GO

--  ÕœÌÀ ”Ã· Õ”» «·„⁄—›
CREATE PROCEDURE Update_Identifier
    @IdentifierID INT,
    @IdentifierTypeID INT,
    @IdentifierValue NVARCHAR(100)
AS
BEGIN
    UPDATE Identifiers
    SET IdentifierTypeID = @IdentifierTypeID,
        IdentifierValue = @IdentifierValue
    WHERE IdentifierID = @IdentifierID;
END
GO

-- Õ–› ”Ã· Õ”» «·„⁄—›
CREATE PROCEDURE Delete_Identifier
    @IdentifierID INT
AS
BEGIN
    DELETE FROM Identifiers
    WHERE IdentifierID = @IdentifierID;
END
GO















-- 1. ≈÷«›… ‘Œ’ ÃœÌœ
CREATE PROCEDURE sp_Person_Insert
    @FirstName NVARCHAR(100),
    @SecondName NVARCHAR(100),
    @ThirdName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentifierID INT,
    @Gender NVARCHAR(10),
    @BirthDate DATE,
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Address NVARCHAR(200),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Persons
        (FirstName, SecondName, ThirdName, LastName, IdentifierID, Gender, BirthDate, Phone, Email, Address, IsActive)
    VALUES
        (@FirstName, @SecondName, @ThirdName, @LastName, @IdentifierID, @Gender, @BirthDate, @Phone, @Email, @Address, @IsActive);

    SELECT SCOPE_IDENTITY() AS NewPersonID;
END
GO

-- 2. Ã·» »Ì«‰«  ‘Œ’ Õ”» «·„⁄—›
CREATE PROCEDURE sp_Person_GetByID
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT PersonID, FirstName, SecondName, ThirdName, LastName, IdentifierID, Gender, BirthDate, Phone, Email, Address, IsActive
    FROM Persons
    WHERE PersonID = @PersonID;
END
GO

-- 3.  ÕœÌÀ »Ì«‰«  ‘Œ’ ﬂ«„·
CREATE PROCEDURE sp_Person_Update
    @PersonID INT,
    @FirstName NVARCHAR(100),
    @SecondName NVARCHAR(100),
    @ThirdName NVARCHAR(100),
    @LastName NVARCHAR(100),
    @IdentifierID INT,
    @Gender NVARCHAR(10),
    @BirthDate DATE,
    @Phone NVARCHAR(20),
    @Email NVARCHAR(100),
    @Address NVARCHAR(200),
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Persons
    SET
        FirstName = @FirstName,
        SecondName = @SecondName,
        ThirdName = @ThirdName,
        LastName = @LastName,
        IdentifierID = @IdentifierID,
        Gender = @Gender,
        BirthDate = @BirthDate,
        Phone = @Phone,
        Email = @Email,
        Address = @Address,
        IsActive = @IsActive
    WHERE PersonID = @PersonID;
END
GO

-- 4. Õ–› ‘Œ’ Õ”» «·„⁄—›
CREATE PROCEDURE sp_Person_Delete
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Persons
    WHERE PersonID = @PersonID;
END
GO

-- 5. Ã·» Ã„Ì⁄ «·√‘Œ«’
CREATE PROCEDURE sp_Person_GetAll
AS
BEGIN
    SET NOCOUNT ON;

    SELECT PersonID, FirstName, SecondName, ThirdName, LastName, IdentifierID, Gender, BirthDate, Phone, Email, Address, IsActive
    FROM Persons;
END
GO

-- 6. Ã·» «·√‘Œ«’ «·‰‘ÿÌ‰ ›ﬁÿ (IsActive = 1)
CREATE PROCEDURE sp_Person_GetActive
AS
BEGIN
    SET NOCOUNT ON;

    SELECT PersonID, FirstName, SecondName, ThirdName, LastName, IdentifierID, Gender, BirthDate, Phone, Email, Address, IsActive
    FROM Persons
    WHERE IsActive = 1;
END
GO

-- 7.  ÕœÌÀ Õ«·… «· ›⁄Ì· ›ﬁÿ (IsActive) Õ”» PersonID
CREATE PROCEDURE sp_Person_UpdateIsActive
    @PersonID INT,
    @IsActive BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Persons
    SET IsActive = @IsActive
    WHERE PersonID = @PersonID;
END
GO















-- 1. ≈Ã—«¡ ·≈÷«›… ”Ã· ÃœÌœ
CREATE PROCEDURE sp_InsertGuardian
    @PersonID INT,
    @Jobs NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    INSERT INTO Guardians (PersonID, Jobs, Notes)
    VALUES (@PersonID, @Jobs, @Notes);

    -- «—Ã«⁄ «·„⁄—› «·ÃœÌœ ··’› «·„œŒ·
    SELECT SCOPE_IDENTITY() AS NewGuardianID;
END
GO

-- 2. ≈Ã—«¡ ·«” —Ã«⁄ Ã„Ì⁄ «·”Ã·« 
CREATE PROCEDURE sp_GetAllGuardians
AS
BEGIN
    SELECT GuardianID, PersonID, Jobs, Notes
    FROM Guardians;
END
GO

-- 3. ≈Ã—«¡ ·«” —Ã«⁄ ”Ã· Õ”» GuardianID
CREATE PROCEDURE sp_GetGuardianByID
    @GuardianID INT
AS
BEGIN
    SELECT GuardianID, PersonID, Jobs, Notes
    FROM Guardians
    WHERE GuardianID = @GuardianID;
END
GO

-- 4. ≈Ã—«¡ · ÕœÌÀ ”Ã· Õ”» GuardianID
CREATE PROCEDURE sp_UpdateGuardian
    @GuardianID INT,
    @PersonID INT,
    @Jobs NVARCHAR(50),
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    UPDATE Guardians
    SET PersonID = @PersonID,
        Jobs = @Jobs,
        Notes = @Notes
    WHERE GuardianID = @GuardianID;
END
GO

-- 5. ≈Ã—«¡ ·Õ–› ”Ã· Õ”» GuardianID
CREATE PROCEDURE sp_DeleteGuardian
    @GuardianID INT
AS
BEGIN
    DELETE FROM Guardians
    WHERE GuardianID = @GuardianID;
END
GO


CREATE PROCEDURE sp_GetGuardiansWithStudents
AS
BEGIN
    SELECT DISTINCT g.GuardianID, g.PersonID, g.Jobs, g.Notes
    FROM Guardians g
    INNER JOIN Students s ON g.GuardianID = s.GuardianID
    ORDER BY g.GuardianID;
END
GO
