using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using TopicTalks.Web.Common;
using TopicTalks.Web.Configs;

namespace TopicTalks.Web.Services;

internal class HttpService(
    IHttpContextAccessor httpContextAccessor, 
    IHttpClientFactory httpClientFactory,
    IOptions<AppSettings> appSettings,
    IAuthService authService) : IHttpService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IAuthService _authService = authService;
    private readonly string _apiBaseUrl = appSettings.Value.ApiBaseUrl;

    public HttpClient Client {
        get {

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_apiBaseUrl);

            if (_httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
            {
                var token = GetToken();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }

    private string GetToken()
    {
        var authHeader = _httpContextAccessor.HttpContext?.Request.Headers.Authorization.ToString();

        // Retrieve token if it exists, otherwise generate a new one
        return authHeader is not null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader["Bearer ".Length..].Trim()
            : _authService.GenerateJwtToken();
    }
}