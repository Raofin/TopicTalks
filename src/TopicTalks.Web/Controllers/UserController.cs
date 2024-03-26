using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Services;
using TopicTalks.Web.VIewModels;

namespace TopicTalks.Web.Controllers;

public class UserController(IAuthService authService) : Controller
{
    private readonly IAuthService _authService = authService;

    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

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

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Index", "Home");
    }

}