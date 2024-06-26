﻿using ErrorOr;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Attributes;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

public class AccountController(IAccountService accountService, IValidator<IFormFile> validator) : ApiController
{
    private readonly IAccountService _userService = accountService;
    private readonly IValidator<IFormFile> _validator = validator;

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegistrationRequest request)
    {
        var registration = await _userService.RegisterAsync(request);

        return registration.IsError switch {
            false => Ok(registration.Value),
            _ => registration.Errors.Any(e => e.Type is ErrorType.Conflict)
                ? Conflict("User with the provided email already exists.")
                : Problem("An unexpected error occurred.")
        };
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var login = await _userService.LoginAsync(request);

        return login.IsError switch {
            false => Ok(login.Value),
            _ => login.Errors.Any(e => e.Type is ErrorType.NotFound or ErrorType.Unauthorized)
                ? Unauthorized("Invalid Credentials.")
                : Problem("An unexpected error occurred.")
        };
    }

    [HttpPatch("password")]
    public async Task<IActionResult> ChangePassword(PasswordChangeRequest request)
    {
        var passwordChange = await _userService.ChangePasswordAsync(User.GetUserId(), request);

        return passwordChange.IsError switch {
            false => Ok("Password was successfully updated."),
            _ => passwordChange.Errors.Any(e => e.Type is ErrorType.Unauthorized)
                ? Unauthorized("Invalid old password.")
                : Problem("An unexpected error occurred.")
        };
    }

    [HttpPut("profileImage")]
    public async Task<IActionResult> ChangeProfileImage(IFormFile file)
    {
        var validationResult = await _validator.ValidateAsync(file);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDto());
        }

        await using var stream = file.OpenReadStream();
        var image = await _userService.ChangeProfileImageAsync(new FileUploadDto(file.FileName, stream,file.ContentType), User.GetUserId());

        return image.IsError
            ? Problem("An unexpected error occurred.")
            : Ok(image.Value);
    }

    [AllowAnonymous]
    [HttpPost("exists")]
    public async Task<IActionResult> CheckUserExists(UserExistsRequest user)
    {
        var exists = await _userService.IsUserExistsAsync(user.Username, user.Email);

        return Ok(new { Exists = exists });
    }

    [HttpGet("profile")]
    public async Task<IActionResult> GetUser()
    {
        var user = await _userService.GetWithDetailsAsync(User.GetUserId());

        return !user.IsError
            ? Ok(user.Value)
            : user.FirstError.Type switch {
                ErrorType.NotFound => NotFound("User was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [AllowAnonymous]
    [HttpGet("basicInfo/{userId}")]
    public async Task<IActionResult> GetBasicInfo(long userId)
    {
        var user = await _userService.GetUserBasicInfo(userId);

        return !user.IsError
            ? Ok(user.Value)
            : user.FirstError.Type switch {
                ErrorType.NotFound => NotFound("User was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyOtp(VerifyRequest? verify)
    {
        if (verify is null)
        {
            await _userService.SendOtpAsync(User.GetEmail());

            return Ok("Otp was sent successfully.");
        }

        var verification = await _userService.VerifyOtpAsync(User.GetEmail(), verify.Code);

        return verification.IsError
            ? BadRequest("Invalid Otp.")
            : Ok(verification.Value);
    }
}
