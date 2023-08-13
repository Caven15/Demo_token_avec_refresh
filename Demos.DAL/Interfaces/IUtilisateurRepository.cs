using Demo.DAL.Data;
using Demos.DAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Interfaces
{
    public interface IUtilisateurRepository
    {
        void RegisterUtilisateur(UtilisateurData data);

        UtilisateurData LoginUtilisateur(string email, string password);

        bool IsRefreshTokenValid(string refreshToken);

        UtilisateurData GetUtilisateurById(int id);
        UtilisateurData GetUtilisateurByEmail(string email);
        void InsertRefreshToken(string email, string refreshToken, DateTime validite);
    }
}
