using System;

namespace Demo.API.models.Forms
{
    public class UtilisateurRegisterForm
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string DateNaissance { get; set; }
        public string Password { get; set; }
    }
}
