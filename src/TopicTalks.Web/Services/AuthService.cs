#pragma warning disable CS8604 // Possible null reference argument
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopicTalks.Web.Configs;

namespace TopicTalks.Web.Services;

internal class AuthService(IHttpContextAccessor httpContextAccessor) : IAuthService
{
    private readonly IHttpContextAccessor _contextAccessor = httpContextAccessor;

    public async Task<bool> SignInWithTokenAsync(string token)
    {
        try
        {
            var principal = await GetPrincipalFromToken(token);

            if (principal == null) return false; // Invalid token

            var properties = new AuthenticationProperties
            {
                AllowRefresh = true, // Allow cookie refresh
                IsPersistent = true, // Persist cookie across sessions
                ExpiresUtc = DateTimeOffset.Now.AddDays(7) // Set cookie expiration to 7 days
            };

            await _contextAccessor.HttpContext.SignInAsync(principal, properties);

            return true; // Sign in successful
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task SignOutAsync()
    {
        await _contextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }


    private async Task<ClaimsPrincipal?> GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            // Define token validation parameters
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true, // Validate the security key when validating the token
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(AppSettingsFetcher.JwtKey)), // Set the security key
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = "https://rawfin.net",
                ValidAudience = "https://rawfin.net",
                RequireExpirationTime = true, // Require to have an expiration time
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

            if (IsJwtWithValidSecurityAlgorithm(validatedToken))
            {
                return principal;
            }
        }
        catch (SecurityTokenValidationException)
        {
            return null;
        }

        return null; // Token validation failed
    }

    private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken)
    {
        // Check if the token is a JWT and uses the expected security algorithm
        return validatedToken is JwtSecurityToken jwtSecurityToken &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature,
                   StringComparison.InvariantCultureIgnoreCase);
    }
}
