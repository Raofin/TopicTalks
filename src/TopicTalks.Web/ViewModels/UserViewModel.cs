namespace TopicTalks.Web.ViewModels;

public record UserViewModel(
    long UserId,
    string Username,
    string Email,
    bool IsVerified,
    DateTime CreatedAt,
    UserDetailsViewModel? UserDetails,
    List<string> Roles
);

public record UserDetailsViewModel(
    string Name,
    string InstituteName,
    string IdCardNumber
);

public record UserBasicInfoViewModel(
    long? UserId,
    string? Username,
    string? Email,
    string? ProfileImageUrl,
    DateTime CreatedAt
);

public record UserRoleViewModel(
    long UserRoleId,
    long RoleId
);