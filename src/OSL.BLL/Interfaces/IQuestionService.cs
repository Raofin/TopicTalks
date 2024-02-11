using ErrorOr;
using OSL.BLL.Models;
using OSL.DAL.Entities;

namespace OSL.BLL.Interfaces;

public interface IQuestionService
{
    Task<ErrorOr<Question>> CreateQuestion(QuestionVM model);
    Task<ErrorOr<long>> DeleteQuestion(long questionId);
    Task<ErrorOr<List<Question>>> Get(string? searchText);
    Task<ErrorOr<Question>> Get(long questionId);
    Task<ErrorOr<List<Question>>> GetMyQuestions(long userId);
    Task<ErrorOr<List<Question>>> GetMyRespondedQuestions(long userId);
    Task<ErrorOr<Question>> UpdateQuestion(QuestionVM model);
}