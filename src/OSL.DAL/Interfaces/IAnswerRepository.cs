using ErrorOr;
using OSL.DAL.Entities;

namespace OSL.DAL.Interfaces;

public interface IAnswerRepository
{
    Task<ErrorOr<Answer>> Create(Answer answer);
    Task<ErrorOr<Answer>> Get(long answerId);
    Task<ErrorOr<List<Answer>>> Get(long questionId, long parentAnswerId);
    Task<ErrorOr<Answer>> Get(long questionId, long answerId, long parentAnswerId);
    Task<ErrorOr<Answer>> Update(Answer updatedAnswer);
    Task<ErrorOr<long>> Delete(long answerId);
    Task<ErrorOr<bool>> HasTeachersAnswer(int questionId);
}