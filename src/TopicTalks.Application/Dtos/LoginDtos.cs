using TopicTalks.Contracts.Common;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string Email,
    string Password,
    RoleName Role
);