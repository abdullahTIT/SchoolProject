-- 1. Insert
CREATE PROCEDURE sp_User_Insert
    @PersonID INT,
    @UserName NCHAR(55),
    @Password NCHAR(55),
    @IsActive BIT
AS
BEGIN
    INSERT INTO Users (PersonID, UserName, Password, IsActive)
    VALUES (@PersonID, @UserName, @Password, @IsActive);

    SELECT SCOPE_IDENTITY() AS NewUserID;
END
GO

-- 2. Update
CREATE PROCEDURE sp_User_Update
    @UserID INT,
    @PersonID INT,
    @UserName NCHAR(55),
    @Password NCHAR(55),
    @IsActive BIT
AS
BEGIN
    UPDATE Users
    SET PersonID = @PersonID,
        UserName = @UserName,
        Password = @Password,
        IsActive = @IsActive
    WHERE UserID = @UserID;
END
GO

-- 3. Delete
CREATE PROCEDURE sp_User_Delete
    @UserID INT
AS
BEGIN
    DELETE FROM Users
    WHERE UserID = @UserID;
END
GO

-- 4. Get by ID
CREATE PROCEDURE sp_User_GetByID
    @UserID INT
AS
BEGIN
    SELECT *
    FROM Users
    WHERE UserID = @UserID;
END
GO



-- 6. Get by Username and Password
CREATE PROCEDURE sp_User_GetByUserNameAndPassword
    @UserName NCHAR(55),
    @Password NCHAR(55)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE UserName = @UserName
      AND Password = @Password;
END
GO

-- 7. Get All
CREATE PROCEDURE sp_User_GetAll
AS
BEGIN
    SELECT *
    FROM Users;
END
GO


-- 5. Get by Username
CREATE PROCEDURE sp_User_GetByUsername
    @UserName NCHAR(55)
AS
BEGIN
    SELECT *
    FROM Users
    WHERE UserName = @UserName;
END
GO
-- 8. Check if Exists by Username
CREATE PROCEDURE sp_User_ExistsByUsername
    @UserName NCHAR(55)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName)
        SELECT 1 AS ExistsResult;
    ELSE
        SELECT 0 AS ExistsResult;
END
GO

-- 9. Check if Exists by Username and Password
CREATE PROCEDURE sp_User_ExistsByUsernameAndPassword
    @UserName NCHAR(55),
    @Password NCHAR(55)
AS
BEGIN
    IF EXISTS (SELECT 1 FROM Users WHERE UserName = @UserName AND Password = @Password)
        SELECT 1 AS ExistsResult;
    ELSE
        SELECT 0 AS ExistsResult;
END
GO
