CREATE PROCEDURE [dbo].[spVerifRefreshToken]
    @RefreshToken NVARCHAR(255)
AS
BEGIN
    DECLARE @CurrentTime DATETIME = GETDATE();
    DECLARE @IsValid BIT = 0;

    IF EXISTS (SELECT 1 FROM [Utilisateur] WHERE RefreshToken = @RefreshToken AND RefreshTokenExpiration > @CurrentTime)
    BEGIN
        -- Le RefreshToken est valide
        SET @IsValid = 1;
    END
    ELSE
    BEGIN
        -- Le RefreshToken est invalide
        SET @IsValid = 0;
    END

    -- Renvoyer la valeur de validation
    SELECT @IsValid AS IsValid;
END
