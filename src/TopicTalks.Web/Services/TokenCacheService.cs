using Microsoft.Extensions.Caching.Memory;
using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Services;

public class TokenCacheService(IMemoryCache memoryCache) : ITokenCacheService
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private const string CacheKeyPrefix = "BearerToken_";

    public string? GetToken(string userId)
    {
        var cacheKey = GetCacheKeyForUser(userId);
        return _memoryCache.Get<string>(cacheKey);
    }

    public void SetToken(string userId, string token)
    {
        var cacheKey = GetCacheKeyForUser(userId);
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(30));

        _memoryCache.Set(cacheKey, token, cacheEntryOptions);
    }

    private string GetCacheKeyForUser(string userId)
    {
        return CacheKeyPrefix + userId;
    }
}
