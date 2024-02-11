using OSL.DAL.Entities;

namespace OSL.BLL.Models;

public class QuestionAnswerVM
{
    public Question Question { get; set; } = null!;
    public List<AnswerVM> AnswerVMs { get; set; } = null!;
}
