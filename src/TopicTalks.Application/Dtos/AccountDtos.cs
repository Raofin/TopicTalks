using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string Email,
    string Password
);

public record AuthenticationResponse(
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

public record PasswordChangeRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record VerifyRequest(string Code);