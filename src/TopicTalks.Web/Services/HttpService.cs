using System.Net.Http.Headers;

namespace TopicTalks.Web.Services;

internal class HttpService(
    IHttpContextAccessor httpContextAccessor, 
    IHttpClientFactory httpClientFactory,
    IAuthService authService) : IHttpService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
    private readonly IAuthService _authService = authService;

    public HttpClient Client {
        get {

            var client = _httpClientFactory.CreateClient("TT_Api");

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