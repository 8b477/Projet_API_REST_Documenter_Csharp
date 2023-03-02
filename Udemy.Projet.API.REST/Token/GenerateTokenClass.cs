using Microsoft.IdentityModel.Tokens;
using Projet.API.REST.Swagger.Token;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JWT.Token
{
    /// <summary>
    /// Classe de génération d'un token
    /// </summary>
    public static class GenerateTokenClass
    {
        /// <summary>
        /// Génère un Token.
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            var token = new JwtSecurityToken
            (
            claims: Array.Empty<Claim>(),
            notBefore: new DateTimeOffset(DateTime.Now).DateTime,
            expires: new DateTimeOffset(DateTime.Now.AddMinutes(60)).DateTime,
            signingCredentials: new SigningCredentials(TokenHelper.SIGNING_KEY, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
