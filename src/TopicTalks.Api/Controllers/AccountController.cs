using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var registration = await _userService.Register(request);

        return registration.IsError switch
        {
            false => Ok(registration.Value),
            _ => registration.Errors.Any(e => e.Type is ErrorType.Conflict)
                ? Conflict("User with the provided email already exists.")
                : Problem("An unexpected error occurred.")
        };
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var login = await _userService.Login(request);

        return login.IsError switch
        {
            false => Ok(login.Value),
            _ => login.Errors.Any(e => e.Type is ErrorType.NotFound or ErrorType.Unauthorized)
                ? Unauthorized("Invalid Credentials.")
                : Problem("An unexpected error occurred.")
        };
    }

    [Authorize]
    [HttpGet("details")]
    public async Task<IActionResult> GetUser()
    {
        var userId = long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var user = await _userService.GetWithDetailsAsync(userId);

        return !user.IsError
            ? Ok(user.Value)
            : user.FirstError.Type switch {
                ErrorType.NotFound => NotFound("User was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [Authorize]
    [HttpGet("excel/users")]
    public async Task<IActionResult> GetExcel()
    {
        var excelFile = await _userService.UserListExcelFile();

        return File(excelFile.Bytes, excelFile.ContentType, excelFile.Name);
    }
}
