using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Models
{
    public class UtilisateurModel
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public string token { get; set; }
        public string RefreshToken { get; set; }
    }
}
