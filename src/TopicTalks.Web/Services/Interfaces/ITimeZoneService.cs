namespace TopicTalks.Web.Services.Interfaces;

public interface ITimeZoneService
{
    DateTime ConvertUtcToUserLocalTime(DateTime utcDateTime);
}