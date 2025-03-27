using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using UserService.DataAccess.Entities;

namespace UserService.Api.Authentication
{
    public class JwtTokenGeneratorService : IJwtTokenGeneratorService
    {
        private readonly JwtSettings _settings;
        public JwtTokenGeneratorService(JwtSettings jwtSettings)
        {
            _settings = jwtSettings;
        }
        public string GeneratorToken(User user)
        {
            var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.PhoneNumber, user.Phone),
            new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            var creds = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
