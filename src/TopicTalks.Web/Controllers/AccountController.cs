using System.Net;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

public class AccountController(IAuthService authService, IHttpService httpService) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IHttpService _httpService = httpService;

    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var response = await _httpService.Client.PostAsync("api/account/login", login.ToStringContent());

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(response.ToJson())!;

            await _authService.SignInWithTokenAsync(loginResponse.Token);

            var x = HttpContext.User;

            return Ok();
        }

        return response.StatusCode == HttpStatusCode.Unauthorized
            ? Unauthorized()
            : BadRequest();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (!ModelState.IsValid)
        {
            return UnprocessableEntity(ModelState);
        }

        var response = await _httpService.Client.PostAsync("api/account/register", register.ToStringContent());

        return response.StatusCode switch {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }
}