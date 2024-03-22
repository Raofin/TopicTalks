using ErrorOr;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Models;

namespace TopicTalks.Application.Interfaces;

public interface IAnswerService
{
    Task<ErrorOr<List<AnswerVM>>> AnswersWithReplies(long questionId, long parentAnswerId = 0);
    Task<ErrorOr<Answer>> Create(AnswerVM model);
    Task<ErrorOr<long>> Delete(long answerId);
    Task<ErrorOr<Answer>> Get(long answerId);
    Task<ErrorOr<bool>> HasTeachersAnswer(int questionId);
    Task<ErrorOr<Answer>> Update(AnswerVM model);
}