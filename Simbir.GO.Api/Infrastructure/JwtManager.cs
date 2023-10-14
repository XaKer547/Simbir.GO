using Microsoft.IdentityModel.Tokens;
using Simbir.GO.Domain.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Simbir.GO.Api.Infrastructure
{
    public class JwtManager : IJwtManager
    {
        private readonly SymmetricSecurityKey _key;

        public JwtManager(IConfiguration configuration)
        {
            var key = configuration["JwtConfiguration:Key"];

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public async Task<string> CreateToken(long userId, string role)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha256),
                Expires = DateTime.Now.AddDays(7),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
