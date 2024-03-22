namespace TopicTalks.Application.Dtos;

public record UserDto(
    long UserId,
    string Email,
    DateTime CreatedAt,
    List<QuestionDtos> Questions,
    List<AnswerDtos> Answers,
    List<UserDetailDto> UserDetails,
    List<UserRoleDto> UserRoles
);

public record UserDetailDto(
    string? Name,
    string? InstituteName,
    string? IdCardNumber
);

public record UserRoleDto(
    long UserRoleId,
    long RoleId
);