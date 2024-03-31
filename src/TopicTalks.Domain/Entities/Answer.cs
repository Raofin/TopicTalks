namespace TopicTalks.Domain.Entities;

public class Answer
{
    public long AnswerId { get; set; }

    public long? ParentAnswerId { get; set; }

    public long QuestionId { get; set; }

    public string Explanation { get; set; } = null!;

    public long? UserId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Question Question { get; set; } = null!;

    public User? User { get; set; }
}