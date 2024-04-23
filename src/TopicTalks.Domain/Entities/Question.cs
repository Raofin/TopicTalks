using System.ComponentModel.DataAnnotations.Schema;

namespace TopicTalks.Domain.Entities;

public class Question
{
    public long QuestionId { get; set; }
    public string Topic { get; set; } = null!;
    public string Explanation { get; set; } = null!;
    public bool IsNotified { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public long? UserId { get; set; }
    public User? User { get; set; }

    public string? ImageFileId { get; set; }
    public CloudFile? ImageFile { get; set; }
    
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();

    [NotMapped]
    public bool HasTeachersResponse { get; set; } = false;
}