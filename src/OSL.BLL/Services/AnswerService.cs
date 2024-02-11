using ErrorOr;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.BLL.Services;

internal class AnswerService(IAnswerRepository _answerRepository) : IAnswerService
{
    public async Task<ErrorOr<Answer>> Create(AnswerVM model)
    {
        try
        {
            var answer = new Answer {
                ParentAnswerId = model.ParentAnswerId,
                QuestionId = model.QuestionId,
                Explanation = model.Explanation,
                UserId = model.UserId
            };

            return await _answerRepository.Create(answer);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<IEnumerable<Answer>>> Get(long questionId)
    {
        try
        {
            return await _answerRepository.Get(questionId);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    /*public async Task<ErrorOr<Answer>> Get(long questionId, long answerId)
    {
        try
        {
            return await _answerRepository.Get(questionId, answerId);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }*/

    public async Task<ErrorOr<Answer>> Update(AnswerVM model)
    {
        try
        {
            var answer = new Answer {
                AnswerId = model.AnswerId,
                ParentAnswerId = model.ParentAnswerId,
                QuestionId = model.QuestionId,
                Explanation = model.Explanation,
                UserId = model.UserId
            };

            return await _answerRepository.Update(answer);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<long>> Delete(long answerId)
    {
        try
        {
            return await _answerRepository.Delete(answerId);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

   public async Task<ErrorOr<List<AnswerVM>>> AnswersWithReplies(long questionId, long parentAnswerId = 0)
    {
        try
        {
            var answers = await _answerRepository.Get(questionId, parentAnswerId);

            if(answers.IsError) throw new Exception();

            var answerModel = answers.Value.Select(ans => new AnswerVM {
                AnswerId = ans.AnswerId,
                ParentAnswerId = ans.ParentAnswerId,
                QuestionId = ans.QuestionId,
                Explanation = ans.Explanation,
                UserId = ans.UserId,
                CreatedAt = ans.CreatedAt
            }).ToList();

            foreach (var item in answerModel)
            {
                var replies = await AnswersWithReplies(questionId, item.AnswerId);
                item.Answers = replies.Value;
            }

            return answerModel;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }
}
