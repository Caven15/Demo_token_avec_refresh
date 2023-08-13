using Demo.DAL.Data;
using Demo.DAL.Interfaces;
using Demo.DAL.Mapper;
using Demos.DAL.Data;
using System;
using System.Linq;
using Tools.Connection;

namespace Demo.DAL.Repositories
{
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly Connection _connection;

        public UtilisateurRepository(Connection connection)
        {
            _connection = connection;
        }

        public UtilisateurData LoginUtilisateur(string email, string password)
        {
            Command command = new Command("spUtilisateurLogin", true);
            command.AddParameter("Email", email);
            command.AddParameter("Password", password);
            return _connection.ExecuteReader(command, er => er.DbToUtilisateur()).SingleOrDefault();
        }

        public void RegisterUtilisateur(UtilisateurData data)
        {
            Command command = new Command("spUtilisateurRegister", true);
            command.AddParameter("Nom", data.Nom);
            command.AddParameter("Prenom", data.Prenom);
            command.AddParameter("Email", data.Email);
            command.AddParameter("DateNaissance", data.DateNaissance);
            command.AddParameter("Password", data.Password);
            _connection.ExecuteNonQuery(command);
        }

        public bool IsRefreshTokenValid(string refreshToken)
        {
            Command command = new Command("spVerifRefreshToken", true);
            command.AddParameter("RefreshToken", refreshToken);
            bool result = (bool)_connection.ExecuteScalar(command);
            Console.WriteLine(result);
            return result;
        }

        public void InsertRefreshToken(string email, string refreshToken, DateTime validite)
        {
            Command command = new Command("spInsertRefreshToken", true);
            command.AddParameter("Email", email);
            command.AddParameter("NewRefreshToken", refreshToken);
            command.AddParameter("RefreshTokenExpiration", validite);
            _connection.ExecuteNonQuery(command);
        }

        public UtilisateurData GetUtilisateurById(int id)
        {
            Command command = new Command("spGetUtilisateurById", true);
            command.AddParameter("Id", id);
            return _connection.ExecuteReader(command, er => er.DbToUtilisateur()).SingleOrDefault();
        }

        public UtilisateurData GetUtilisateurByEmail(string email)
        {
            Command command = new Command("spGetUtilisateurByEmail", true);
            command.AddParameter("Email", email);
            return _connection.ExecuteReader(command, er => er.DbToUtilisateur()).SingleOrDefault();
        }

    }
}
