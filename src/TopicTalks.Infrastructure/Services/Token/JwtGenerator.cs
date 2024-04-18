using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services.Token;

internal class JwtGenerator(IOptions<JwtSettings> jwtSettings) : IJwtGenerator
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, user.Email),
            new("UserId", user.UserId.ToString()),
            new("IsVerified", user.IsVerified.ToString(), ClaimValueTypes.Boolean),
        };

        user.UserRoles.ToList().ForEach(role => claims.Add(new Claim(ClaimTypes.Role, ((RoleType)role.RoleId).ToString())));

        var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

        var token = new JwtSecurityToken(
                issuer: "https://rawfin.net",
                audience: "https://rawfin.net",
                claims: claims,
                expires: DateTime.Now.AddDays(7),
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
