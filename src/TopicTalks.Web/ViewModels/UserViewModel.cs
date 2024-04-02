namespace TopicTalks.Web.ViewModels;

public record UserBasicInfoViewModel(
    long? UserId,
    string? Email
);

public record UserViewModel(
    long UserId,
    string Email,
    DateTime CreatedAt,
    UserDetailsViewModel? UserDetails,
    List<string> Roles
);

public record UserDetailsViewModel(
    string Name,
    string InstituteName,
    string IdCardNumber
);

public record UserRoleViewModel(
    long UserRoleId,
    long RoleId
);