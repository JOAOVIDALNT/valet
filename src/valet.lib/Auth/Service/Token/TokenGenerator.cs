using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using valet.lib.Auth.Domain.Entities;
using valet.lib.Auth.Domain.Interfaces;

namespace valet.lib.Auth.Service.Token
{
    internal class TokenGenerator : TokenHandler, ITokenGenerator
    {
        private readonly string _secretKey;
        private readonly uint _expirationMinutes;

        public TokenGenerator(string secretKey, uint expirationMinutes)
        {
            _secretKey = secretKey ?? throw new ArgumentNullException(nameof(secretKey));
            _expirationMinutes = expirationMinutes;
        }

        public string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var credentials = new SigningCredentials(SecurityKey(_secretKey), SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                SigningCredentials = credentials,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        private ClaimsIdentity GenerateClaims(User user)
        {
            var claims = new ClaimsIdentity();

            claims.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
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
