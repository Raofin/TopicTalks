using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Core;

public interface IJwtGenerator
{
    string GenerateJwtToken(User user);
}