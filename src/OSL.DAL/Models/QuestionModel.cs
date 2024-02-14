using OSL.DAL.Entities;

namespace OSL.DAL.Models;

public class QuestionModel
{
    public Question Question { get; set; } = null!;
    public bool HasTeachersResponse { get; set; } = false;
}
