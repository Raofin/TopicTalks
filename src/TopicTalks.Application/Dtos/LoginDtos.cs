using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string Email,
    string Password,
    RoleType Role
);

public record LoginResponse(
    long UserId,
    string Token,
    string Email,
    UserDetailDto? UserDetails,
    List<RoleType> Role
);