namespace TopicTalks.Domain.Entities;

public class Answer
{
    public long AnswerId { get; set; }
    public long? ParentAnswerId { get; set; }
    public string Explanation { get; set; } = null!;
    public bool IsNotified { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    
    public long? UserId { get; set; }
    public User? User { get; set; }

    public long QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}