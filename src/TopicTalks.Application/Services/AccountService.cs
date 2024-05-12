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
    IEmailSender emailService,
    ICloudService cloudService,
    IPdfGenerator pdfGenerator) : IAccountService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHashPassword _passwordService = passwordService;
    private readonly IJwtGenerator _tokenService = tokenService;
    private readonly IEmailSender _emailService = emailService;
    private readonly ICloudService _cloudService = cloudService;
    private readonly IPdfGenerator _pdfGenerator = pdfGenerator;

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
            ImageFileId = request.ImageFileId,
            UserRoles = new List<UserRole> {
                new() { RoleId = (long)request.Role, }
            },
            UserDetails = request?.UserDetails is null ? null
                : new UserDetail {
                    FullName = request.UserDetails.Name,
                    InstituteName = request.UserDetails.InstituteName,
                    IdCardNumber = request.UserDetails.IdCardNumber,
                }
        };

        _unitOfWork.User.Add(user);

        var otpCode = GenerateOtp();

        var otp = new Otp {
            Email = user.Email,
            Code = otpCode,
            ExpiresAt = DateTime.UtcNow.AddMinutes(5),
        };

        _unitOfWork.Otp.Add(otp);

        await _unitOfWork.CommitAsync();
        await _unitOfWork.Entry(user).Reference(u => u.ImageFile).LoadAsync();

        _emailService.SendWelcomeWithOtp(user.Email, otpCode);

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

    public async Task<ErrorOr<CloudFileDto>> ChangeProfileImageAsync(FileUploadDto dto, long userId)
    {
        var user = await _unitOfWork.User.GetAsync(userId);

        if (user is null)
        {
            return Error.Unexpected();
        }

        var cloudFile = user.ImageFileId is not null
            ? await _cloudService.UpdateAsync(user.ImageFileId, dto, commit: false)
            : await _cloudService.UploadAsync(dto, userId, commit: false);

        user.ImageFileId = cloudFile.CloudFileId;

        await _unitOfWork.CommitAsync();

        return cloudFile.ToDto()!;
    }

    public async Task<byte[]> GenerateUserListPdfAsync()
    {
        var users = await _unitOfWork.User.GetWithDetailsAsync();
        
        return await _pdfGenerator.UserListPdf(users);
    }

    #region ### OTP ###

    public async Task SendOtpAsync(string email)
    {
        var code = GenerateOtp();

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

        var user = await _unitOfWork.User.GetWithDetailsAsync(email);
        user!.IsVerified = true;

        await _unitOfWork.CommitAsync();
        _emailService.SendVerified(email);

        return Authenticate(user);
    }

    public async Task<ErrorOr<UserBasicInfoDto>> GetUserBasicInfo(long userId)
    {
        var user = await _unitOfWork.User.GetBasicInfoAsync(userId);

        return user is null
            ? Error.NotFound()
            : user.ToBasicInfoDto();
    }

    private static string GenerateOtp()
    {
        return new Random().Next(1000, 9999).ToString();
    }

    #endregion ### OTP ###
}
