using ErrorOr;
using OSL.BLL.Models;
using OSL.DAL.Entities;

namespace OSL.BLL.Interfaces;

public interface IAnswerService
{
    Task<ErrorOr<List<AnswerVM>>> AnswersWithReplies(long questionId, long parentAnswerId = 0);
    Task<ErrorOr<Answer>> Create(AnswerVM model);
    Task<ErrorOr<long>> Delete(long answerId);
    Task<ErrorOr<IEnumerable<Answer>>> Get(long questionId);
    //Task<ErrorOr<Answer>> Get(long questionId, long answerId);
    Task<ErrorOr<Answer>> Update(AnswerVM model);
}