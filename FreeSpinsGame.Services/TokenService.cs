using FreeSpinsGame.Services.Interfaces;

using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FreeSpinsGame.Data.Models;

namespace FreeSpinsGame.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey securityKey;

        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]!));
        }

        public string CreateToken(Player player)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, player.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, player.Email!),
                new Claim(JwtRegisteredClaimNames.GivenName, player.UserName!)
            };

            var credentials = new SigningCredentials(this.securityKey, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = credentials,
                Issuer = this.configuration["JWT:Issuer"],
                Audience = this.configuration["JWT:Audience"]
            };

            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();

            var token = securityTokenHandler.CreateToken(tokenDescriptor);

            return securityTokenHandler.WriteToken(token);
        }
    }
}
