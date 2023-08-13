CREATE PROCEDURE [dbo].[spGetUtilisateurByEmail]
	@Email NVARCHAR(100)
AS
	SELECT *
	FROM [Utilisateur]
	WHERE Email = @Email
RETURN 0
