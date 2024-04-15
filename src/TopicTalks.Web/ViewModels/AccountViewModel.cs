using TopicTalks.Web.Enums;

namespace TopicTalks.Web.ViewModels;

public record LoginViewModel(
    string Email,
    string Password,
    RoleType Role
);

public record AuthenticationResponse(
    string Token,
    UserViewModel User
);

public record RegisterViewModel(
    string Email,
    string Password,
    string ConfirmPassword,
    RoleType Role,
    UserDetailsViewModel? UserDetails
);

public record PasswordChangeViewModel(
    string OldPassword,
    string NewPassword,
    string ConfirmPassword
);

public record VerifyViewModel(string Code);