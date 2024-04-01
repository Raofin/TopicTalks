namespace TopicTalks.Web.ViewModels;

public record AnswerViewModel(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfo? UserInfo
);

public record AnswerRequestViewModel(
    long? ParentAnswerId,
    long QuestionId,
    string Explanation
);

public record AnswerUpdateRequestViewModel(
    long AnswerId,
    string Explanation
);

public record AnswerWithRepliesViewModel(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfo? UserInfo,
    List<AnswerWithRepliesViewModel> Answers
);