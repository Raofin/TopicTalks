namespace TopicTalks.Web.ViewModels;

public record QuestionViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    UserBasicInfoViewModel? UserInfo,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);

public record QuestionCreateViewModel(
    string Topic,
    string Explanation
);

public record QuestionUpdateViewModel(
    string QuestionId,
    string Topic,
    string Explanation
);

public record QuestionWithAnswersViewModel(
    long QuestionId,
    string Topic,
    string Explanation,
    bool HasTeachersResponse,
    UserBasicInfoViewModel? UserInfo,
    List<AnswerWithRepliesViewModel> Answers,
    DateTime CreatedAt,
    DateTime? UpdatedAt
);