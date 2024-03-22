using TopicTalks.Web.Entities;

namespace TopicTalks.Web.Models;

public class QuestionAnswerVM
{
    public Question Question { get; set; } = null!;
    public List<AnswerVM> AnswerVMs { get; set; } = null!;
    public bool HasTeachersAnswer { get; set; } = false;
}
