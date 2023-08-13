using Demo.BLL.Models;
using Demo.DAL.Data;
using Demos.BLL.Models;
using Demos.DAL.Data;

namespace Demo.BLL.Mapper
{
    public static class Mapper
    {
        internal static UtilisateurData BllToDal(this UtilisateurModel model)
        {
            return new UtilisateurData()
            {
                Id = model.Id,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Email = model.Email,
                DateNaissance = model.DateNaissance,
                Password = model.Password,
                IsAdmin = model.IsAdmin
            };
        }

        internal static UtilisateurModel DalToBll(this UtilisateurData data)
        {
            if (data is null) return null;
            return new UtilisateurModel()
            {
                Id = data.Id,
                Nom = data.Nom,
                Prenom = data.Prenom,
                Email = data.Email,
                DateNaissance = data.DateNaissance,
                Password = data.Password,
                IsAdmin = data.IsAdmin
            };
        }

        internal static UtilisateurLoginModel DalToBll(this UtilisateurLoginData data)
        {
            if (data is null) return null;
            return new UtilisateurLoginModel()
            {
                Email = data.Email,
                Password = data.Password
            };
        }
    }
}
