CREATE PROCEDURE [dbo].[spGetUtilisateurById]
	@Id int = 0
AS
	SELECT * 
	FROM [Utilisateur]
	WHERE Id = @Id
RETURN 0