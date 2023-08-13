CREATE PROCEDURE [dbo].[spInsertRefreshToken]
    @Email NVARCHAR(255),
    @NewRefreshToken NVARCHAR(255),
    @RefreshTokenExpiration DATETIME
AS
BEGIN
    UPDATE [Utilisateur]
    SET RefreshToken = @NewRefreshToken,
        RefreshTokenExpiration = @RefreshTokenExpiration
    WHERE Email = @Email;

    -- Retourner un code de succès (par convention, 0 indique succès)
    RETURN 0;
END
