using System;

namespace Demo.API.models
{
    public class UserWithToken
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public bool IsAdmin { get; set; }
    }
}
