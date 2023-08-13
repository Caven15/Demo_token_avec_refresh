
using Demo.API.Infrastructure;
using Demo.API.Mapper;
using Demo.API.models;
using Demo.API.models.Forms;
using Demo.BLL.Interfaces;
using Demo.BLL.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUtilisateurService _utilisateurService;
        private readonly TokenManager _tokenManager;

        public AuthController(IUtilisateurService utilisateurService, TokenManager tokenManager)
        {
            _utilisateurService = utilisateurService;
            _tokenManager = tokenManager;
        }

        [HttpPost(nameof(Register))]
        public IActionResult Register(UtilisateurRegisterForm form)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _utilisateurService.RegisterUtilisateur(form.ApiToBll());
            return Ok();
        }



        [HttpPost(nameof(Login))]
        public IActionResult Login(UtilisateurLoginForm form)
        {
            try
            {
                if (!ModelState.IsValid) 
                    return BadRequest(ModelState);

                UtilisateurModel currentuser = _utilisateurService.LoginUtilisateur(form.Email, form.Password);

                if (currentuser is null) 
                    return NotFound("Utilisateur n'existe pas ...");

                string refreshToken = _tokenManager.GenerateJWT(currentuser, 180);
                string token = _tokenManager.GenerateJWT(currentuser, 30);

                DateTime validiteRefresh = DateTime.Now.AddSeconds(180);

                _utilisateurService.InsertRefreshToken(currentuser.Email, refreshToken, validiteRefresh);

                UserWithToken utilisateur = new UserWithToken
                {
                    Id = currentuser.Id,
                    Nom = currentuser.Nom,
                    Prenom = currentuser.Prenom,
                    Email = currentuser.Email,
                    DateNaissance = currentuser.DateNaissance,
                    Token = token,
                    RefreshToken = refreshToken
                };

                return Ok(utilisateur);
            }

            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost(nameof(RefreshToken))]
        public IActionResult RefreshToken(RefreshTokenModel refreshToken)
        {
            TokenModel newTokens = new TokenModel();
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                // je vérifie si le token correspond avec la db
                if ((bool)_utilisateurService.IsRefreshTokenValid(refreshToken.RefreshToken))
                {
                    // récupération des infos par rapport au refresh
                    string email = _tokenManager.GetEmailFromJwtToken(refreshToken.RefreshToken);
                    UtilisateurModel user = _utilisateurService.GetUtilisateurByEmail(email);
                    
                    //Vérification de la validité du token de rafraîchissement actuel
                    if (_utilisateurService.IsRefreshTokenValid(refreshToken.RefreshToken))
                    {
                        // je regénère le nouveau token
                        string token = _tokenManager.GenerateJWT(user, 30);
                        string NewRefreshToken = _tokenManager.GenerateJWT(user, 180);

                        // ajout des valeurs avant retours
                        newTokens.Token = token;
                        newTokens.RefreshToken = NewRefreshToken;
                    }

                    return Ok(newTokens);
                }
                else
                {
                    return BadRequest("Le token de rafraîchissement n'est pas valide.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
