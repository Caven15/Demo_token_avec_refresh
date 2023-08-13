using Demo.DAL.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Mapper
{
    internal static class Mapper
    {
        internal static UtilisateurData DbToUtilisateur(this IDataRecord record)
        {
            return new UtilisateurData()
            {
                Id = (int)record["Id"],
                Nom = (string)record["Nom"],
                Prenom = (string)record["Prenom"],
                Email = (string)record["Email"],
                DateNaissance = (DateTime)record["DateNaissance"],
                IsAdmin = (bool)record["IsAdmin"]
            };
        }
    }
}
