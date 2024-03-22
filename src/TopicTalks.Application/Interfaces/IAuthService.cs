using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface IAuthService
{
    string UserEmail { get; }
    string UserId { get; }
    string UserRole { get; }

    string GenerateJwtToken(User user);
}