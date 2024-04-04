using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Extensions;

public static class HttpExtensions
{
    public static string ToJson(this HttpResponseMessage response)
    {
        return response.Content.ReadAsStringAsync().Result;
    }

    public static T? DeserializeTo<T>(this HttpResponseMessage response)
    {
        var json = response.Content.ReadAsStringAsync().Result;
        return JsonConvert.DeserializeObject<T>(json);
    }

    public static StringContent ToStringContent(this object obj)
    {
        return new StringContent(
            content: JsonConvert.SerializeObject(obj),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );
    }

    public static string? UserId(this HttpContext http)
    {
        return http.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    }

    public static string? UserEmail(this HttpContext http)
    {
        return http.User.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static string? UserRole(this HttpContext http)
    {
        return http.User.FindFirst(ClaimTypes.Role)?.Value;
    }

    public static async Task<ExcelFile> ToExcelFile(this HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync();
        return new ExcelFile(bytes);
    }
}