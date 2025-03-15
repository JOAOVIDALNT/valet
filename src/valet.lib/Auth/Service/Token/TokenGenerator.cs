using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces;

namespace valet.lib.Auth.Service.Token
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly string _secretKey;

        public TokenGenerator(string secretKey) => _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_secretKey);

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = credentials,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private static ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            claims.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            foreach (var role in user.UserRoles)
            {
                claims.AddClaim(new Claim(ClaimTypes.Role, role.Role!.Name));
            }

            return claims;
        }
    }
}
