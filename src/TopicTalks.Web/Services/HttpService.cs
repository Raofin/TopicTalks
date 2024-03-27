#pragma warning disable CS8602 // Dereference of a possibly null reference

using System.Net.Http.Headers;
using static TopicTalks.Web.Configs.AppSettingsFetcher;

namespace TopicTalks.Web.Services;

internal class HttpService(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IAuthService authService) : IHttpService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IAuthService _authService = authService;

    public HttpClient Client {
        get {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(ApiBaseUrl);


            if (_httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated == true)
            {
                var token = GetTokenFromHttpContext();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return client;
        }
    }

    private string GetTokenFromHttpContext()
    {
        var authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();

        // Retrieve token if it exists, otherwise generate a new one
        return authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase) 
            ? authHeader["Bearer ".Length..].Trim() 
            : _authService.GenerateJwtToken();
    }
}