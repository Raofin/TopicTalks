namespace TopicTalks.Web.ViewModels;

public record UserViewModel(
    long UserId,
    string Username,
    string Email,
    bool IsVerified,
    DateTime CreatedAt,
    CloudFileViewModel? ImageFile,
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
    List<string>? Roles,
    DateTime CreatedAt
);

public record UserRoleViewModel(
    long UserRoleId,
    long RoleId
);

public class UserInfoCookies
{
    public long UserId { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public CloudFileViewModel? ImageFile { get; set; }
    public UserDetailsViewModel? UserDetails { get; set; }
    public List<string> Roles { get; set; } = null!;
}