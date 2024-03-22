using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Models;

namespace TopicTalks.Domain.Models;

public class QuestionAnswerVM
{
    public Question Question { get; set; } = null!;
    public List<AnswerVM> AnswerVMs { get; set; } = null!;
    public bool HasTeachersAnswer { get; set; } = false;
}
