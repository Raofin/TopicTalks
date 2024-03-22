using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace TopicTalks.Web.Extensions;

public static class HttpContextExtensions
{
    public static string? UserId(this HttpContext http)
    {
        return http.User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
    }

    public static string? UserEmail(this HttpContext http)
    {
        return http.User.FindFirst(JwtRegisteredClaimNames.Email)?.Value;
    }

    public static string? UserRole(this HttpContext http)
    {
        return http.User.FindFirst(ClaimTypes.Role)?.Value.ToString();
    }
}
