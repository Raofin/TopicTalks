using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Services;
using TopicTalks.Web.VIewModels;

namespace TopicTalks.Web.Controllers;

public class AuthController(IAuthService authService, IHttpService httpService) : Controller
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
        var isSignInSuccessful = await _authService.SignInWithTokenAsync(login.Token);

        return isSignInSuccessful ? Ok() : Unauthorized();
    }

    /*[HttpGet("register")]
    public IActionResult Register(RegistrationViewModel registration)
    {
        return View();
    }*/

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("asd")]
    public async Task<IActionResult> Register()
    {
        var response = await _httpService.Client.GetAsync("https://localhost:5001/api/User/UserAuth?userId=1");

        return Ok(response.Content.ReadAsStringAsync().Result);
    }

}