using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string Email,
    string Password,
    RoleType Role
);

public record LoginResponse(
    string Token,
    UserDto User
);