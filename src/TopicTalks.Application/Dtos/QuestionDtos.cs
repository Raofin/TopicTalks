using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Dtos;

public record QuestionRequestDto(
    long QuestionId,
    string Topic,
    string Explanation,
    long UserId
);

public record QuestionResponseWithTeacherDto(
    long QuestionId,
    string Topic,
    string Explanation,
    bool? HasTeachersResponse,
    UserBasicInfo? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionResponseDto(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfo? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);