using ErrorOr;
using OSL.BLL.Models;
using OSL.DAL.Entities;

namespace OSL.BLL.Interfaces;

public interface IQuestionService
{
    Task<ErrorOr<Question>> CreateQuestion(QuestionVM model);
    Task<ErrorOr<long>> DeleteQuestion(long questionId);
    Task<ErrorOr<IEnumerable<Question>>> Get();
    Task<ErrorOr<Question>> Get(long questionId);
    Task<ErrorOr<Question>> UpdateQuestion(QuestionVM model);
}