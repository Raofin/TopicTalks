using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface IJwtService
{
    string GenerateJwtToken(User user);
}