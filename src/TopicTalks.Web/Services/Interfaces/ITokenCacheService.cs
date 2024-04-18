namespace TopicTalks.Web.Services.Interfaces;

public interface ITokenCacheService
{
    string? GetToken(string userId);
    void SetToken(string userId, string token);
}