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
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    RoleType Role,
    UserDetailDto? UserDetails,
    string? ImageFileId
);

public record PasswordChangeRequest(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record UserExistsRequest(string? Username, string? Email);

public record VerifyRequest(string Code);