using TopicTalks.Web.Enums;

namespace TopicTalks.Web.ViewModels;

public record RegisterViewModel(
    string Email,
    string Password,
    string ConfirmPassword,
    RoleType Role,
    UserDetailsViewModel? UserDetails
);