CREATE PROCEDURE [dbo].[spUtilisateurLogin]
    @Email NVARCHAR(100),
    @Password VARCHAR(50)
AS

BEGIN
    SET NOCOUNT OFF;

    DECLARE @PasswordHash BINARY(64), @SecurityStamp UNIQUEIDENTIFIER;



    SET @SecurityStamp = (SELECT SecurityStamp FROM [Utilisateur] WHERE Email = @Email)
    SET @PasswordHash = dbo.fHasher(@Password, @SecurityStamp)

    IF EXISTS (SELECT TOP 1 * FROM [Utilisateur] WHERE Email = @Email AND PasswordHash = @PasswordHash)
    BEGIN
        SELECT * INTO #TempUser
        FROM [Utilisateur]
        WHERE Email LIKE @Email
        ALTER TABLE #TempUser
        DROP COLUMN PasswordHash, SecurityStamp
        SELECT * FROM #TempUser
        DROP TABLE #TempUser
    END

    RETURN 0
END