using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FonApi.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace FonApi.Service{
    public class JwtService : IJwtService {
        private readonly string _secretKey;
        private readonly string _issuer;

        public JwtService(string secretKey, string issuer) {
            _secretKey = secretKey;
            _issuer = issuer;
        }

        public string GenerateToken(int userId, string email)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Convert.FromBase64String(_secretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _issuer,
                _issuer,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}