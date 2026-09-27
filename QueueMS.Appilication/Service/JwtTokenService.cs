using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

using QueueMS.Appilication.Interfaces;
using QueueMS.Appilication.JwtSetting;
using QueueMS.Domain.Models.UserModels;

using System.Security.Claims;
using System.Text;


namespace QueueMS.Appilication.Service
{
    public class JwtTokenService(IOptions<JwtSettings> jwtSettings) : IJwtTokenService
    {
        private readonly JwtSettings _settings = jwtSettings.Value;


        public (string Token, DateTime ExpiresAt) CreateToken(User user, IEnumerable<string> role)
        {
            var expiresAt = DateTime.UtcNow.AddMinutes(_settings.ExpiryMinutes);

            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Email, user.Email!),
                new(JwtRegisteredClaimNames.Name, user.UserName!),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.AddRange(role.Select(role => new Claim(ClaimTypes.Role, role)));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiresAt,
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = credentials
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(descriptor);

            return (token, expiresAt);
        }
    }
}
