using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace TopicTalks.Web.Extensions;

public static class HttpExtensions
{
    public static string ToJson(this HttpResponseMessage response)
    {
        return response.Content.ReadAsStringAsync().Result;
    }

    public static StringContent ToStringContent(this object obj)
    {
        return new StringContent(
            content: JsonSerializer.Serialize(obj),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );
    }

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
        return http.User.FindFirst(ClaimTypes.Role)?.Value;
    }
}
