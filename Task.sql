CREATE TABLE Task (
    id smallint primary key identity (1, 1),
    title nvarchar(100) not null,
    description nvarchar(100) not null,
    isCompleted bit not null,
    createdAt datetime not null
)
GO;


CREATE PROCEDURE usp_AddTask @title nvarchar(100), @description nvarchar(100)
AS
    INSERT INTO Task (title, description, isCompleted, createdAt)
    OUTPUT INSERTED.*
    VALUES (@title, @description, 0, GETDATE())
GO;


CREATE PROCEDURE usp_GetAllTasks
AS
    SELECT * FROM Task
    ORDER BY id
GO;


CREATE PROCEDURE usp_GetTaskById @id smallint
AS
    SELECT * FROM Task
    WHERE id = @id
GO;


CREATE PROCEDURE usp_UpdateTask @id smallint, @title nvarchar(100), @description nvarchar(100), @isCompleted bit
AS
    UPDATE Task SET
        title = @title,
        description = @description,
        isCompleted = @isCompleted
    OUTPUT INSERTED.*
    WHERE id = @id
GO;


CREATE PROCEDURE usp_DeleteTask @id smallint
AS
    DELETE FROM Task
    WHERE id = @id
GO;
