using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class AccountService(
    IUnitOfWork unitOfWork, 
    IHashPassword passwordService, 
    IJwtGenerator tokenService, 
    IEmailSender emailService) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHashPassword _passwordService = passwordService;
    private readonly IJwtGenerator _tokenService = tokenService;
    private readonly IEmailSender _emailService = emailService;

    public async Task<bool> IsUserExistsAsync(string? username, string? email)
    {
        return await _unitOfWork.User.IsUserExistsAsync(username, email);
    }

    public async Task<ErrorOr<AuthenticationResponse>> RegisterAsync(RegistrationRequest request)
    {
        if (await IsUserExistsAsync(request.Username, request.Email))
        {
            return Error.Conflict();
        }

        var (hashedPassword, salt) = _passwordService.HashPasswordWithSalt(request.Password);

        var user = new User {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = hashedPassword,
            Salt = salt,
            ImageFileId = request.ImageFileId
        };

        user.UserRoles.Add(new UserRole {
            RoleId = (long)request.Role,
        });

        if (request?.UserDetails != null)
        {
            user.UserDetails = new UserDetail {
                FullName = request.UserDetails.Name,
                InstituteName = request.UserDetails.InstituteName,
                IdCardNumber = request.UserDetails.IdCardNumber,
            };
        }

        _unitOfWork.User.Add(user);
        await _unitOfWork.CommitAsync();

        _emailService.SendWelcome(user.Email);
        await SendOtpAsync(user.Email);

        return Authenticate(user);
    }

    public async Task<ErrorOr<AuthenticationResponse>> LoginAsync(LoginRequest request)
    {
        var user = await _unitOfWork.User.GetWithDetailsAsync(request.UsernameOrEmail);

        if (user == null)
        {
            return Error.NotFound();
        }

        return _passwordService.VerifyPassword(user.PasswordHash, user.Salt, request.Password)
            ? Authenticate(user) 
            : Error.Unauthorized();
    }

    private AuthenticationResponse Authenticate(User user)
    {
        return new AuthenticationResponse(
            Token: _tokenService.GenerateJwtToken(user),
            User: user.ToDto()
        );
    }

    public async Task<ErrorOr<UserDto>> GetWithDetailsAsync(long userId)
    {
        var user = await _unitOfWork.User.GetWithDetailsAsync(userId);

        return user is null ? Error.NotFound() : user.ToDto();
    }

    public async Task<ErrorOr<Success>> ChangePasswordAsync(long userId, PasswordChangeRequest request)
    {
        var user = await _unitOfWork.User.GetAsync(userId);

        if (user is null)
        {
            return Error.Unexpected();
        }

        var isPasswordVerified = _passwordService.VerifyPassword(user.PasswordHash, user.Salt, request.OldPassword);

        if (!isPasswordVerified)
        {
            return Error.Unauthorized();
        }

        var (hashedPassword, salt) = _passwordService.HashPasswordWithSalt(request.NewPassword);

        user.PasswordHash = hashedPassword;
        user.Salt = salt;

        await _unitOfWork.CommitAsync();

        return Result.Success;
    }

    #region ### OTP ###

    public async Task SendOtpAsync(string email)
    {
        var code = new Random().Next(1000, 9999).ToString();

        var otp = await _unitOfWork.Otp.GetOtpAsync(email);

        if (otp is not null)
        {
            _unitOfWork.Otp.Remove(otp);
        }

        _unitOfWork.Otp.Add(new Otp {
            Email = email,
            Code = code,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        });

        await _unitOfWork.CommitAsync();
        _emailService.SendOtp(email, code);
    }

    public async Task<ErrorOr<AuthenticationResponse>> VerifyOtpAsync(string email, string code)
    {
        var otp = await _unitOfWork.Otp.GetValidOtpAsync(email, code);

        if (otp is null)
        {
            return Error.Unauthorized();
        }

        _unitOfWork.Otp.Remove(otp);

        var user = await _unitOfWork.User.GetByEmailAsync(email);
        user.IsVerified = true;

        await _unitOfWork.CommitAsync();
        _emailService.SendVerified(email);

        return Authenticate(user);
    }

    #endregion ### OTP ###
}
