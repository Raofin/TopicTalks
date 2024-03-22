using ErrorOr;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Models;

namespace TopicTalks.Domain.Interfaces;

public interface IQuestionRepository
{
    Task<ErrorOr<Question>> CreateQuestion(Question question);
    Task<ErrorOr<long>> Delete(long questionId);
    Task<ErrorOr<Question>> Get(long questionId);
    Task<ErrorOr<List<QuestionModel>>> Get(string? searchText);
    Task<ErrorOr<List<Question>>> GetMyQuestions(long userId);
    Task<ErrorOr<List<Question>>> GetMyRespondedQuestions(long userId);
    Task<ErrorOr<Question>> Update(Question updatedQuestion);
}