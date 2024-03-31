using System.ComponentModel.DataAnnotations.Schema;

namespace TopicTalks.Domain.Entities;

public class Question
{
    public long QuestionId { get; set; }

    public string Topic { get; set; } = null!;

    public string Explanation { get; set; } = null!;

    public long? UserId { get; set; }

    [NotMapped]
    public bool HasTeachersResponse { get; set; } = false;

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public User? User { get; set; }
}