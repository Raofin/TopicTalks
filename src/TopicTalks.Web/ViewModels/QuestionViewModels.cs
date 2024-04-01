namespace TopicTalks.Web.ViewModels;

public record QuestionViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfo? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionRequestViewModel(
    string Topic,
    string Explanation
);

public record QuestionWithAnswersViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    bool HasTeachersResponse,
    UserBasicInfo? UserInfo,
    List<AnswerWithRepliesViewModel> Answers,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);