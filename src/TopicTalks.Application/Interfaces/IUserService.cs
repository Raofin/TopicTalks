using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IUserService
{
    Task<ErrorOr<AuthenticationResponse>> Register(RegistrationRequest request);
    Task<ErrorOr<AuthenticationResponse>> Login(LoginRequest request);
    Task<ErrorOr<UserDto>> GetWithDetailsAsync(long userId);
    Task<ExcelFile> UserListExcelFile();
    Task SendOtp(string email);
    Task<ErrorOr<AuthenticationResponse>> VerifyOtp(string email, string code);
    Task<ErrorOr<Success>> ChangePassword(long userId, PasswordChangeRequest request);
}