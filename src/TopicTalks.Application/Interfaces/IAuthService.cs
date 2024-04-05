using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface IAuthService
{
    string GenerateJwtToken(User user);
}