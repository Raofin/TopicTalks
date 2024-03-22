using TopicTalks.Web.Entities;

namespace TopicTalks.Web.Models;

public class QuestionModel
{
    public Question Question { get; set; } = null!;
    public bool HasTeachersResponse { get; set; } = false;
}
