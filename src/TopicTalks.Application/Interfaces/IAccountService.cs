using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IAccountService
{
    Task<bool> IsUserExistsAsync(string? username, string? email);
    Task<ErrorOr<AuthenticationResponse>> RegisterAsync(RegistrationRequest request);
    Task<ErrorOr<AuthenticationResponse>> LoginAsync(LoginRequest request);
    Task<ErrorOr<UserDto>> GetWithDetailsAsync(long userId);
    Task<ErrorOr<Success>> ChangePasswordAsync(long userId, PasswordChangeRequest request);
    Task SendOtpAsync(string email);
    Task<ErrorOr<AuthenticationResponse>> VerifyOtpAsync(string email, string code);
}