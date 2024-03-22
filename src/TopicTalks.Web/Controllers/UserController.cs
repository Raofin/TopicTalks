using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace TopicTalks.Web.Controllers;

public class UserController(IHttpContextAccessor _httpAccessor) : Controller
{
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

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        if (_httpAccessor.HttpContext != null)
        {
            await _httpAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        return Ok();
    }
}