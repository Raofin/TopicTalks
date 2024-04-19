using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Services;

internal class TimeZoneService(IHttpContextAccessor httpContextAccessor) : ITimeZoneService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DateTime ConvertUtcToUserLocalTime(DateTime utcDateTime)
    {
        string? timeZoneId = null;
        if (_httpContextAccessor.HttpContext?.Request.Cookies["TimeZone"] != null)
        {
            timeZoneId = _httpContextAccessor.HttpContext.Request.Cookies["TimeZone"]!;
        }

        if (string.IsNullOrEmpty(timeZoneId))
        {
            return utcDateTime;
        }

        var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);

        return localTime;
    }
}