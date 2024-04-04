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

public record PasswordChangeRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);