namespace TopicTalks.Application.Dtos;

public record QuestionDto(
    long QuestionId,
    string Topic,
    string Explanation,
    long UserId
);

public record QuestionRequestDto(
    string Topic,
    string Explanation
);

public record QuestionUpdateRequestDto(
    long QuestionId,
    string Topic,
    string Explanation
);

public record QuestionResponseDto(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfo? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionWithAnswersDto(
    long QuestionId,
    string Topic,
    string Explanation,
    bool? HasTeachersResponse,
    UserBasicInfo? UserInfo,
    List<AnswerWithRepliesDto>? Answers,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);