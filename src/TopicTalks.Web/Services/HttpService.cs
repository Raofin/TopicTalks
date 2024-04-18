using System.Net.Http.Headers;
using System.Security.Claims;
using TopicTalks.Web.Services.Interfaces;

namespace TopicTalks.Web.Services;

internal class HttpService(
    IHttpClientFactory httpClientFactory,
    IHttpContextAccessor httpContextAccessor,
    ITokenCacheService tokenCacheService,
    IAuthService authService) : IHttpService
{
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ITokenCacheService _tokenCacheService = tokenCacheService;
    private readonly IAuthService _authService = authService;

    public HttpClient Client {
        get {
            var client = _httpClientFactory.CreateClient("TT_Api");

            if (_httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirstValue("UserId")!;
                var token = GetToken(userId);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }

    private string GetToken(string userId)
    {
        var cachedToken = _tokenCacheService.GetToken(userId);

        if (string.IsNullOrEmpty(cachedToken))
        {
            var newToken = _authService.GenerateJwtToken();
            _tokenCacheService.SetToken(userId, newToken);
            return newToken;
        }

        return cachedToken;
    }
}