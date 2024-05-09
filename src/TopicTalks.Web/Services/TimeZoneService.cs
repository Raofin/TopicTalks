using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Services;

internal class TimeZoneService(IHttpContextAccessor httpContextAccessor) : ITimeZoneService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DateTime ConvertUtcToUserLocalTime(DateTime utcDateTime)
    {
        if (_httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue("TimeZone", out var timeZoneId) is true)
        {
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            return localTime;
        }

        return utcDateTime;
    }
}