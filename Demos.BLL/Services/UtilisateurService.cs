using Demo.BLL.Interfaces;
using Demo.BLL.Mapper;
using Demo.BLL.Models;
using Demo.DAL.Interfaces;
using Demos.BLL.Models;
using Demos.DAL.Data;
using System;

namespace Demo.BLL.Services
{
    public class UtilisateurService : IUtilisateurService
    {
        private readonly IUtilisateurRepository _utilisateurRepository;
        public UtilisateurService(IUtilisateurRepository utilisatreurRepository)
        {
            _utilisateurRepository = utilisatreurRepository;
        }

        public UtilisateurModel LoginUtilisateur(string email, string password)
        {
            return _utilisateurRepository.LoginUtilisateur(email, password)?.DalToBll();
        }

        public bool IsRefreshTokenValid(string refreshToken)
        {
            return _utilisateurRepository.IsRefreshTokenValid(refreshToken);
        }

        public void RegisterUtilisateur(UtilisateurModel model)
        {
            _utilisateurRepository.RegisterUtilisateur(model.BllToDal());
        }

        public UtilisateurModel GetUtilisateurById(int id)
        {
            return _utilisateurRepository.GetUtilisateurById(id).DalToBll();
        }

        public UtilisateurModel GetUtilisateurByEmail(string id)
        {
            return _utilisateurRepository.GetUtilisateurByEmail(id).DalToBll();
        }

        public void InsertRefreshToken(string email, string refreshToken, DateTime validite)
        {
            _utilisateurRepository.InsertRefreshToken(email, refreshToken, validite);
        }

    }
}
