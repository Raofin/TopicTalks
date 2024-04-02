using ErrorOr;
using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface IUserService
{
    Task<bool> IsEmailExists(string email);
    Task<bool> IsUserExists(long userId);
    Task<ErrorOr<RegistrationResponse>> Register(RegistrationRequest request);
    Task<ErrorOr<LoginResponse>> Login(LoginRequest request);
    Task<ErrorOr<UserDto>> GetWithDetailsAsync(long userId);
}