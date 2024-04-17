using System.Net;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Attributes;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

[Authorize]
public class AccountController(IAuthService authService, IHttpService httpService) : Controller
{
    private readonly IAuthService _authService = authService;
    private readonly IHttpService _httpService = httpService;

    [AllowAnonymous]
    [RedirectIfAuthenticated]
    [HttpGet("login")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        var response = await _httpService.Client.PostAsync("api/account/login", login.ToStringContent());

        if (response.IsSuccessStatusCode)
        {
            var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(response.ToJson())!;

            await _authService.SignInWithTokenAsync(authResponse.Token);

            return Ok(authResponse.User);
        }

        return new StatusCodeResult((int)response.StatusCode);
    }

    [RedirectIfAuthenticated]
    [HttpGet("register")]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        var response = await _httpService.Client.PostAsync("api/account/register", register.ToStringContent());

        if (response.IsSuccessStatusCode)
        {
            var authResponse = JsonConvert.DeserializeObject<AuthenticationResponse>(response.ToJson())!;

            await _authService.SignInWithTokenAsync(authResponse.Token);

            return Ok(authResponse.User);
        }

        return new StatusCodeResult((int)response.StatusCode);
    }

    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var response = await _httpService.Client.GetAsync("api/account/profile");

        return response.IsSuccessStatusCode
            ? View(response.DeserializeTo<UserViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [Authorize]
    [HttpGet("change-password")]
    public IActionResult ChangePassword()
    {
        return View();
    }

    [Authorize]
    [HttpPatch("password")]
    public async Task<IActionResult> ChangePassword(PasswordChangeViewModel passwordChange)
    {
        var response = await _httpService.Client.PatchAsync("api/account/password", passwordChange.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok()
            : new StatusCodeResult((int)response.StatusCode);
    }

    [HttpGet("AdditionalFields")]
    public IActionResult LoadAdditionalFields()
    {
        return PartialView("_AdditionalFields");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        await _authService.SignOutAsync();
        return RedirectToAction("Dashboard", "Home");
    }

    [Authorize]
    [HttpPost("verify")]
    public async Task<IActionResult> Verify(VerifyViewModel? verify)
    {
        var payload = verify?.Code == null ? null : verify?.ToStringContent();
        var response = await _httpService.Client.PostAsync("api/account/verify", payload);

        if (response.IsSuccessStatusCode)
        {
            if (payload != null)
            {
                var token = JsonConvert.DeserializeObject<AuthenticationResponse>(response.ToJson())!.Token;

                await _authService.SignInWithTokenAsync(token);
            }

            return Ok();
        }

        return new StatusCodeResult((int)response.StatusCode);
    }

    [Authorize]
    [HttpGet("verify")]
    public IActionResult Verify()
    {
        return View();
    }

    [AuthorizeModerator]
    [HttpGet("excel/users")]
    public async Task<IActionResult> GetExcel()
    {
        var response = await _httpService.Client.GetAsync("api/account/excel/users");

        if (response.StatusCode == HttpStatusCode.OK)
        {
            var excelFile = await response.ToExcelFile();
            return File(excelFile.Bytes, excelFile.ContentType, excelFile.Name);
        }

        return new StatusCodeResult((int)response.StatusCode);
    }
}