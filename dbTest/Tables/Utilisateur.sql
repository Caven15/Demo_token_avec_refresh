CREATE TABLE [dbo].[Utilisateur]
(
	[Id] INT NOT NULL IDENTITY, 
    [Nom] NVARCHAR(80) NOT NULL, 
    [Prenom] NVARCHAR(80) NOT NULL, 
    [Email] NVARCHAR(100) NOT NULL, 
    [DateNaissance] DATETIME2 NOT NULL, 
    [PasswordHash] BINARY(64) NULL, 
    [SecurityStamp] UNIQUEIDENTIFIER NULL,
    [RefreshToken] NVARCHAR(255) NULL,
    [RefreshTokenExpiration] DATETIME2 NULL,
    [IsAdmin] BIT NOT NULL DEFAULT 0, 

    CONSTRAINT [PK_Utilisateur] PRIMARY KEY ([Id])

)