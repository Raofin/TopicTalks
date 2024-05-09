using Microsoft.AspNetCore.Http;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

public class UserInfoProvider(IHttpContextAccessor httpContextAccessor) : IUserInfoProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public string? Username()
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst("Username")?.Value;
    }
    
    public DateTime UtcToUserLocalTime(DateTime utcDateTime)
    {
        if (_httpContextAccessor.HttpContext?.Request.Headers.TryGetValue("TimeZone", out var timeZoneId) is true)
        {
            var localTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId!);
            var localTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);
            return localTime;
        }

        return utcDateTime;
    }

    public DateTime UserLocalTimeNow()
    {
        return UtcToUserLocalTime(DateTime.UtcNow);
    }
    
    public long? UserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("UserId");

        if (userIdClaim != null && long.TryParse(userIdClaim.Value, out var userId))
        {
            return userId;
        }

        return null;
    }
}