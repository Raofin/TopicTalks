namespace TopicTalks.Application.Dtos;

public record AnswerDto(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    long UserId
);

public record AnswerRequestDto(
    long? ParentAnswerId,
    long QuestionId,
    string Explanation
);

public record AnswerResponseDto(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfo? UserInfo
);

// for recursive calls
public class AnswerWithRepliesDto
{
    public long AnswerId { get; set; }
    public long? ParentAnswerId { get; set; }
    public string Explanation { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public UserBasicInfo? UserInfo { get; set; }
    public List<AnswerWithRepliesDto>? Answers { get; set; }
}