using TopicTalks.Web.Common;

namespace TopicTalks.Web.ViewModels;

public record LoginViewModel(
    string UsernameOrEmail,
    string Password
);

public record AuthenticationResponse(
    string Token,
    UserViewModel User
);

public record RegisterViewModel(
    string Username,
    string Email,
    string Password,
    string ConfirmPassword,
    RoleType Role,
    UserDetailsViewModel? UserDetails,
    string? ImageFileId
);

public record PasswordChangeViewModel(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record UserExistsViewModel(string? Username, string? Email);

public record VerifyViewModel(string Code);