namespace TopicTalks.Application.Dtos;

public record UserBasicInfoDto(
    long UserId,
    string Email
);

public record UserDto(
    long UserId,
    string Email,
    DateTime CreatedAt,
    UserDetailDto? UserDetails,
    List<string> Roles
);

public record UserDetailDto(
    string Name,
    string InstituteName,
    string IdCardNumber
);

public record UserRoleDto(
    long UserRoleId,
    long RoleId
);