namespace TopicTalks.Web.ViewModels;

public record QuestionViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfoViewModel? UserInfo,
    CloudFileViewModel? ImageFile,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionCreateViewModel(
    string Topic,
    string Explanation,
    string? ImageFileId
);

public record QuestionUpdateViewModel(
    string QuestionId,
    string Topic,
    string Explanation,
    CloudFileViewModel? ImageFile
);

public record QuestionWithAnswersViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    bool HasTeachersResponse,
    UserBasicInfoViewModel? UserInfo,
    List<AnswerWithRepliesViewModel> Answers,
    CloudFileViewModel? ImageFile,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);