namespace TopicTalks.Application.Dtos;

public record QuestionCreateDto(
    string Topic,
    string Explanation,
    string? ImageFileId
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
    bool IsNotified,
    UserBasicInfoDto? UserInfo,
    CloudFileDto? ImageFile,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionWithAnswersDto(
    long QuestionId,
    string Topic,
    string Explanation,
    bool IsNotified,
    bool? HasTeachersResponse,
    UserBasicInfoDto? UserInfo,
    List<AnswerWithRepliesDto>? Answers,
    CloudFileDto? ImageFile,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);