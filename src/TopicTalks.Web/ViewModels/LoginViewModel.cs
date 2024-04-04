using TopicTalks.Web.Enums;

namespace TopicTalks.Web.ViewModels;

public record LoginViewModel(
    string Email,
    string Password,
    RoleType Role
);

public record LoginResponse(
    string Token,
    UserViewModel User
);