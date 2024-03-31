namespace TopicTalks.Application.Dtos;

public record QuestionRequestDto(
    long QuestionId,
    string Topic,
    string Explanation,
    long UserId
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
    List<AnswerResponseDto>? Answers,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);