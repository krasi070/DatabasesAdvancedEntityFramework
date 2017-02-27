CREATE PROCEDURE usp_GetOlder(@Id int)
AS
BEGIN
	UPDATE Minions
	SET Age = Age + 1
	WHERE Id = @Id
END