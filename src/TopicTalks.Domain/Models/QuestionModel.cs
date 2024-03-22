using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Models;

public class QuestionModel
{
    public Question Question { get; set; } = null!;
    public bool HasTeachersResponse { get; set; } = false;
}
