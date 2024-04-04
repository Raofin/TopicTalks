using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Application.Interfaces.Excel;
using TopicTalks.Domain;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Services;

internal class UserService(
    IUnitOfWork unitOfWork, 
    IPasswordService passwordService, 
    IAuthService tokenService, 
    IExcelExportService excelExportService) : IUserService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IPasswordService _passwordService = passwordService;
    private readonly IAuthService _tokenService = tokenService;
    private readonly IExcelExportService _excelExportService = excelExportService;

    public async Task<bool> IsEmailExists(string email)
    {
        return await _unitOfWork.User.IsEmailExists(email);
    }

    public async Task<bool> IsUserExists(long userId)
    {
        return await _unitOfWork.User.IsUserExists(userId);
    }

    public async Task<ErrorOr<UserDto>> GetWithDetailsAsync(long userId)
    {
        var user = await _unitOfWork.User.GetWithDetailsAsync(userId);

        if (user is null)
        {
            return Error.NotFound();
        }

        var userDto = new UserDto(
                UserId: user.UserId,
                Email: user.Email,
                CreatedAt: user.CreatedAt,
                UserDetails: user.UserDetails.ToDto(),
                Roles: user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList()
            );

        return userDto;
    }

    public async Task<ErrorOr<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (await IsEmailExists(request.Email))
        {
            return Error.Conflict();
        }

        var (hashedPassword, salt) = _passwordService.HashPasswordWithSalt(request.Password);

        var user = new User {
            Email = request.Email,
            PasswordHash = hashedPassword,
            Salt = salt,
        };

        user.UserRoles.Add(new UserRole {
            RoleId = (long)request.Role,
        });

        if (request?.UserDetails != null)
        {
            user.UserDetails = new UserDetail {
                Name = request.UserDetails.Name,
                InstituteName = request.UserDetails.InstituteName,
                IdCardNumber = request.UserDetails.IdCardNumber,
            };
        }

        await _unitOfWork.User.AddAsync(user);
        await _unitOfWork.CommitAsync();

        var response = new RegistrationResponse(
                UserId: user.UserId,
                Email: user.Email,
                UserDetails: user.UserDetails.ToDto(),
                Role: user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList()
            );

        return response;
    }

    public async Task<ErrorOr<LoginResponse>> Login(LoginRequest request)
    {
        var user = await _unitOfWork.User.GetWithDetailsAsync(request.Email, (long)request.Role);

        if (user == null)
        {
            return Error.NotFound();
        }

        var isUserVerified = _passwordService.VerifyPassword(user.PasswordHash, user.Salt, request.Password);

        if (!isUserVerified)
        {
            return Error.Unauthorized();
        }

        var response = new LoginResponse(
                Token: _tokenService.GenerateJwtToken(user),
                User: new UserDto(
                    UserId: user.UserId,
                    Email: user.Email,
                    UserDetails: user.UserDetails.ToDto(),
                    Roles: user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList(),
                    CreatedAt: user.CreatedAt
                )
            );

        return response;
    }

    public async Task<ExcelFile> UserListExcelFile()
    {
        var users = await _unitOfWork.User.GetWithDetailsAsync();

        var usersDto = users.Select(u => new UserDto(
                    UserId: u.UserId,
                    Email: u.Email,
                    CreatedAt: u.CreatedAt,
                    UserDetails: u.UserDetails.ToDto(),
                    Roles: u.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList()
                )
            ).ToList();

        return _excelExportService.UserListExcel(usersDto);
    }

    public async Task<ErrorOr<Success>> ChangePassword(long userId, PasswordChangeRequest request)
    {
        var user = await _unitOfWork.User.GetAsync(userId);

        if (user is null)
        {
            return Error.Unexpected();
        }

        var isUserVerified = _passwordService.VerifyPassword(user.PasswordHash, user.Salt, request.OldPassword);

        if (!isUserVerified)
        {
            return Error.Unauthorized();
        }

        var (hashedPassword, salt) = _passwordService.HashPasswordWithSalt(request.NewPassword);

        user.PasswordHash = hashedPassword;
        user.Salt = salt;

        await _unitOfWork.CommitAsync();

        return Result.Success;
    }
}
