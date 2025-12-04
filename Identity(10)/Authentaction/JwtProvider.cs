using Identity_10_.Models.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Identity_10_.Authentaction
{
    public class JwtProvider(IOptions<JwtOptions> options) : IJwtProvider
    {
        private readonly JwtOptions _options = options.Value;

        public (string token, int expireInMinutes) GenerateJwtToken(AppUser user)
        {
            Claim[] Claims =
            [
                new(JwtRegisteredClaimNames.Sub , user.Id.ToString()),
                new(JwtRegisteredClaimNames.GivenName , user.FirstName),
                new(JwtRegisteredClaimNames.FamilyName , user.LastName),
                new(JwtRegisteredClaimNames.Name , user.UserName!),
                new(JwtRegisteredClaimNames.Email , user.Email!),
                new(JwtRegisteredClaimNames.Jti , Guid.NewGuid().ToString())
            ];
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
            var signingCredintials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expiresIn = 30;
            var token = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: Claims,
                expires: DateTime.UtcNow.AddMinutes(expiresIn),
                signingCredentials :signingCredintials
                );
            return (token : new JwtSecurityTokenHandler().WriteToken(token), expiresIn);
        }
    }
}
