Create procedure [dbo].[AddNewUser]
(
 @Name varchar(50),
 @Username varchar(50),
 @Password varchar(50)
 )
 AS
	BEGIN
		INSERT INTO Users VALUES (@Name, @Username, @Password)
END
