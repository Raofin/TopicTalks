using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record LoginRequest(
    string UsernameOrEmail,
    string Password
);

public record AuthenticationResponse(
    string Token,
    UserDto User
);

public record RegistrationRequest(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    string? ImageFileId,
    RoleType Role,
    UserDetailDto? UserDetails
);

public record PasswordChangeRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record UserExistsRequest(string? Username, string? Email);

public record VerifyRequest(string Code);