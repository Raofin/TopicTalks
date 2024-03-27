using TopicTalks.Contracts.Common;

namespace TopicTalks.Application.Dtos;

public record RegistrationRequest(
    string Email,
    string Password,
    string ConfirmPassword,
    RoleName Role,
    UserDetailDto? UserDetails
);

public record RegistrationResponse(
    long UserId,
    string Email,
    UserDetailDto? UserDetails,
    List<RoleName?> Role
);