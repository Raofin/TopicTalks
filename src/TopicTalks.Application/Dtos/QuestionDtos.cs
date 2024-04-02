namespace TopicTalks.Application.Dtos;

public record QuestionCreateDto(
    string Topic,
    string Explanation
);

public record QuestionUpdateDto(
    long QuestionId,
    string Topic,
    string Explanation
);

public record QuestionResponseDto(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfoDto? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionWithAnswersDto(
    long QuestionId,
    string Topic,
    string Explanation,
    bool? HasTeachersResponse,
    UserBasicInfoDto? UserInfo,
    List<AnswerWithRepliesDto>? Answers,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);