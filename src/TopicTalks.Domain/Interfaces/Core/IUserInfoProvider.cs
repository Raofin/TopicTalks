namespace TopicTalks.Domain.Interfaces.Core;

public interface IUserInfoProvider
{
    string? Username();
    DateTime UtcToUserLocalTime(DateTime utcDateTime);
    DateTime UserLocalTimeNow();
}