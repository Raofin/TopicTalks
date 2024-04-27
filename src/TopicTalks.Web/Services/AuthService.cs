using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopicTalks.Web.Common;
using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Services;

internal class AuthService(IHttpContextAccessor httpContextAccessor, IOptions<AppSettings> appSettings) : IAuthService
{
    private readonly ClaimsPrincipal _principal = httpContextAccessor.HttpContext!.User;
    private readonly HttpContext _httpContext = httpContextAccessor.HttpContext!;
    private readonly JwtSettings _jwtSettings = appSettings.Value.JwtSettings;

    public string GenerateJwtToken()
    {
        try
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Email, _principal.FindFirstValue(ClaimTypes.Email)!),
                new("UserId", _principal.Claims.Single(c => c.Type == "UserId").Value),
                new("Username", _principal.Claims.Single(c => c.Type == "Username").Value),
                new("IsVerified", _principal.Claims.Single(c => c.Type == "IsVerified").Value, ClaimValueTypes.Boolean)
            };

            _principal.Claims.Where(c => c.Type == ClaimTypes.Role).ToList().ForEach(claims.Add);

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                    issuer: "https://rawfin.net",
                    audience: "https://rawfin.net",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SignInWithTokenAsync(string token)
    {
        try
        {
            var principal = GetPrincipalFromToken(token);

            var properties = new AuthenticationProperties {
                AllowRefresh = true,
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(7)
            };

            await SignOutAsync();
            await _httpContext.SignInAsync(principal, properties);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SignOutAsync()
    {
        await _httpContext.SignOutAsync();
    }


    private ClaimsPrincipal GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Define token validation parameters
            var validationParameters = new TokenValidationParameters {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "https://rawfin.net",
                ValidAudience = "https://rawfin.net",
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            return IsJwtWithValidSecurityAlgorithm(validatedToken) 
                ? principal 
                : throw new Exception("Invalid JWT.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        // Check if the token is a JWT and uses the expected security algorithm
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}
