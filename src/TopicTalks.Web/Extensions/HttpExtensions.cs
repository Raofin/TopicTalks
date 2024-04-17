using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using TopicTalks.Web.Common;
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

    public static async Task<ExcelFile> ToExcelFile(this HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync();
        return new ExcelFile(bytes);
    }

    public static StringContent ToStringContent(this object obj)
    {
        return new StringContent(
            content: JsonConvert.SerializeObject(obj),
            encoding: Encoding.UTF8,
            mediaType: "application/json"
        );
    }

    public static string? UserId(this HttpContext httpContext)
    {
        return httpContext.User.FindFirst("UserId")?.Value;
    }

    public static string? UserEmail(this HttpContext httpContext)
    {
        return httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
    }

    public static bool IsUserVerified(this HttpContext httpContext)
    {
        return bool.Parse(httpContext.User.FindFirst("IsVerified")?.Value ?? "false");
    }

    public static List<RoleType> UserRole(this HttpContext httpContext)
    {
        return httpContext.User
            .FindAll(ClaimTypes.Role)
            .Select(c => Enum.Parse<RoleType>(c.Value))
            .ToList();
    }
}