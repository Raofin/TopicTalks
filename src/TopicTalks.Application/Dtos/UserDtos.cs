using TopicTalks.Domain.Enums;

namespace TopicTalks.Application.Dtos;

public record UserDto(
    long UserId,
    string Username,
    string Email,
    bool IsVerified,
    DateTime CreatedAt,
    CloudFileDto? ImageFile,
    UserDetailDto? UserDetails,
    List<RoleType> Roles
);

public record UserDetailDto(
    string Name,
    string InstituteName,
    string IdCardNumber
);

public record UserBasicInfoDto(
    long UserId,
    string Username,
    string Email,
    string? ProfileImageUrl,
    DateTime CreatedAt
);

public record UserRoleDto(
    long UserRoleId,
    long RoleId
);