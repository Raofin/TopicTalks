using ErrorOr;
using OSL.DAL.Entities;

namespace OSL.DAL.Interfaces;

public interface IQuestionRepository
{
    Task<ErrorOr<Question>> CreateQuestion(Question question);
    Task<ErrorOr<List<Question>>> Get(string? searchText);
    Task<ErrorOr<Question>> Get(long questionId);
    Task<ErrorOr<Question>> Update(Question updatedQuestion);
    Task<ErrorOr<long>> Delete(long questionId);
}