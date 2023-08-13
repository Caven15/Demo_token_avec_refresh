using System.Security.Claims;
using System.Text;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Demo.API.Infrastructure
{
    public class TokenManager
    {
        private readonly IConfiguration _configuration;

        public TokenManager(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // Génération du jwt
        public string GenerateJWT(dynamic user,int secondsValidity)
        {
            if (user.Email is null)
                throw new ArgumentNullException();

            //Création des crédentials
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._configuration["Jwt:Key"]));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Création de l'objet contenant les informations à stocker dans le token
            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, $"{user.Nom} {user.Prenom}"),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("IsAdmin", user.IsAdmin.ToString()),
                new Claim(ClaimTypes.Role, user.GetType().Name),
                new Claim("Validity", DateTime.UtcNow.AddSeconds(secondsValidity).ToString())
            };

            //Génération du token => Nuget : System.IdentityModel.Tokens.Jwt
            JwtSecurityToken token = new JwtSecurityToken(
                claims: myClaims,
                expires: DateTime.UtcNow.AddSeconds(secondsValidity),
                signingCredentials: credentials,
                issuer: this._configuration["Jwt:myIssuer"],
                audience: this._configuration["Jwt:myAudience"]
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }


        // récupération d'information dans le token
        public string GetEmailFromJwtToken(string token)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            // Utilisez la méthode ReadJwtToken pour lire le token sans validation
            JwtSecurityToken decodedToken = tokenHandler.ReadJwtToken(token);

            // Recherche la revendication Email (ou toute autre revendication que vous cherchez)
            Claim emailClaim = decodedToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Email);

            if (emailClaim != null)
            {
                return emailClaim.Value;
            }
            else
            {
                return null; // Revendication Email non trouvée dans le token
            }
        }

        public static string GetJwtTokenFromRequest(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.ContainsKey("Authorization"))
            {
                string authHeader = httpContext.Request.Headers["Authorization"].FirstOrDefault();

                if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
                {
                    string token = authHeader.Substring("Bearer ".Length).Trim();
                    return token;
                }
            }

            return null;
        }
    }

}
