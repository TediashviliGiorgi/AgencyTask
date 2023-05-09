-- Creating a new user
CREATE PROCEDURE CreateUser
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @RoleId INT
AS
BEGIN
    INSERT INTO Users (Email, Password, RoleId)
    VALUES (@Email, @Password, @RoleId)

    SELECT SCOPE_IDENTITY() AS Id
END
GO

-- Assigning a role to a user
CREATE PROCEDURE AssignRoleToUser
    @UserId INT,
    @RoleId INT
AS
BEGIN
    UPDATE Users
    SET RoleId = @RoleId
    WHERE Id = @UserId
END
GO

-- Changing user data
CREATE PROCEDURE UpdateUser
    @UserId INT,
    @Email NVARCHAR(255),
    @Password NVARCHAR(255),
    @RoleId INT
AS
BEGIN
    UPDATE Users
    SET Email = @Email, Password = @Password, RoleId = @RoleId
    WHERE Id = @UserId
END
GO

-- Changing role for the user
CREATE PROCEDURE UpdateUserRole
    @UserId INT,
    @RoleId INT
AS
BEGIN
    UPDATE Users
    SET RoleId = @RoleId
    WHERE Id = @UserId
END
GO

-- Removing a record of a specific user (with reference to assigned roles)
CREATE PROCEDURE DeleteUser
    @UserId INT
AS
BEGIN
    DELETE FROM Users
    WHERE Id = @UserId
END
GO

-- Filtering the list of users (by specifying the assigned roles)
CREATE PROCEDURE GetUsersByRole
    @RoleId INT
AS
BEGIN
    SELECT * FROM Users
    WHERE RoleId = @RoleId
END
GO
