using ErrorOr;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Models;

namespace TopicTalks.Application.Interfaces;

public interface IQuestionService
{
    Task<ErrorOr<Question>> CreateQuestion(QuestionVM model);
    Task<ErrorOr<long>> DeleteQuestion(long questionId);
    Task<ErrorOr<Question>> Get(long questionId);
    Task<ErrorOr<List<QuestionModel>>> Get(string? searchText);
    Task<ErrorOr<List<Question>>> GetMyQuestions(long userId);
    Task<ErrorOr<List<Question>>> GetMyRespondedQuestions(long userId);
    Task<ErrorOr<Question>> UpdateQuestion(QuestionVM model);
}