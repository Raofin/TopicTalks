namespace TopicTalks.Application.Dtos;

public record AnswerCreateDto(
    long? ParentAnswerId,
    long QuestionId,
    string Explanation
);

public record AnswerUpdateDto(
    long AnswerId,
    string Explanation
);

public record AnswerResponseDto(
    long AnswerId,
    long? ParentAnswerId,
    long QuestionId,
    string Explanation,
    DateTime CreatedAt,
    UserBasicInfoDto? UserInfo
);

// for recursive calls
public class AnswerWithRepliesDto
{
    public long AnswerId { get; set; }
    public long? ParentAnswerId { get; set; }
    public string Explanation { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public UserBasicInfoDto? UserInfo { get; set; }
    public List<AnswerWithRepliesDto>? Answers { get; set; }
}