using Demo.BLL.Models;
using Demos.BLL.Models;
using Demos.DAL.Data;
using System;

namespace Demo.BLL.Interfaces
{
    public interface IUtilisateurService
    {
        void RegisterUtilisateur(UtilisateurModel model);

        UtilisateurModel LoginUtilisateur(string email, string password);

        bool IsRefreshTokenValid(string refreshToken);

        void InsertRefreshToken(string email, string refreshToken, DateTime validite);

        UtilisateurModel GetUtilisateurById(int id);
        UtilisateurModel GetUtilisateurByEmail(string email);
    }
}
