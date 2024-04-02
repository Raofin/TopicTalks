namespace TopicTalks.Web.ViewModels;

public record AnswerViewModel(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfoViewModel? UserInfo
);

public record AnswerCreateViewModel(
    long? ParentAnswerId,
    long QuestionId,
    string Explanation
);

public record AnswerUpdateViewModel(
    long AnswerId,
    string Explanation
);

public record AnswerWithRepliesViewModel(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfoViewModel? UserInfo,
    List<AnswerWithRepliesViewModel> Answers
);