-- Õ–› «·⁄„Êœ IsActive
ALTER TABLE Persons
DROP COLUMN IsActive;

ALTER TABLE Persons DROP CONSTRAINT DF__Persons__IsActiv__3E52440B;

-- ≈÷«›… «·⁄„Êœ ImagePath Êﬁ«»· ·√‰ ÌﬂÊ‰ NULL
ALTER TABLE Persons
ADD ImagePath NVARCHAR(500) NULL;




SELECT 
    name AS ProcedureName
FROM 
    sys.objects
WHERE 
    type = 'P' -- Ì⁄‰Ì ›ﬁÿ «·≈Ã—«¡«  «·„Œ“‰…
    AND OBJECT_DEFINITION(object_id) LIKE '%Persons%';


	
EXEC sp_helptext 'sp_Person_GetAll';
EXEC sp_helptext 'sp_Person_GetAll';
EXEC sp_helptext 'sp_Person_GetAll';
EXEC sp_helptext 'sp_Person_GetAll';



SELECT 
    o.name AS ProcedureName,
    m.definition AS ProcedureDefinition
FROM 
    sys.objects o
JOIN 
    sys.sql_modules m ON o.object_id = m.object_id
WHERE 
    o.type = 'P' -- ··≈Ã—«¡«  «·„Œ“‰… (Stored Procedure)
    AND o.name IN (
        'sp_Person_Insert',
        'sp_Person_GetByID',
        'sp_Person_Update',
        'sp_Person_Delete',
        'sp_Person_GetAll',
        'sp_Person_GetActive',
        'sp_Person_UpdateIsActive'
    );



alter PROCEDURE [dbo].[sp_Person_Delete]
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Persons WHERE PersonID = @PersonID;

    -- «—Ã⁄ ⁄œœ «·’›Ê› «· Ì  „ Õ–›Â«
    SELECT @@ROWCOUNT AS RowsDeleted;
END
go




--  „ Õ–› IsActive° ·–« Â–« «·≈Ã—«¡ €Ì— ’«·Õ ÊÌÃ» Õ–›Â √Ê  Ã«Â·Â
DROP PROCEDURE IF EXISTS sp_Person_GetActive;

alter PROCEDURE sp_Person_GetAll
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Persons;
END
go



alter PROCEDURE sp_Person_GetByID
    @PersonID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM Persons WHERE PersonID = @PersonID;
END
go
alter PROCEDURE sp_Person_Insert
    @FirstName NVARCHAR(20),
    @SecondName NVARCHAR(20),
    @ThirdName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @IdentifierID INT = NULL,
    @Gender BIT,
    @BirthDate DATE = NULL,
    @Phone NCHAR(10),
    @Email NCHAR(100) = NULL,
    @Address NVARCHAR(255) = NULL,
    @ImagePath NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Persons (
        FirstName, SecondName, ThirdName, LastName,
        IdentifierID, Gender, BirthDate, Phone,
        Email, Address, ImagePath
    )
    VALUES (
        @FirstName, @SecondName, @ThirdName, @LastName,
        @IdentifierID, @Gender, @BirthDate, @Phone,
        @Email, @Address, @ImagePath
    );

    SELECT SCOPE_IDENTITY() AS NewPersonID;
END





alter PROCEDURE sp_Person_Update
    @PersonID INT,
    @FirstName NVARCHAR(20),
    @SecondName NVARCHAR(20),
    @ThirdName NVARCHAR(20),
    @LastName NVARCHAR(20),
    @IdentifierID INT = NULL,
    @Gender BIT,
    @BirthDate DATE = NULL,
    @Phone NCHAR(10),
    @Email NCHAR(100) = NULL,
    @Address NVARCHAR(255) = NULL,
    @ImagePath NVARCHAR(255) = NULL
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
        ImagePath = @ImagePath
    WHERE PersonID = @PersonID;
END
