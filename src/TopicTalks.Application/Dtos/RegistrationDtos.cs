using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record RegistrationRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    RoleType Role,
    UserDetailDto? UserDetails
);

public record RegistrationResponse(
    long UserId,
    string Email,
    UserDetailDto? UserDetails,
    List<RoleType> Role
);