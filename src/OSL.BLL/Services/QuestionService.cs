using ErrorOr;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.BLL.Services;

internal class QuestionService(IQuestionRepository _questionRepository) : IQuestionService
{
    public async Task<ErrorOr<Question>> CreateQuestion(QuestionVM model)
    {
        try
        {
            var question = new Question {
                Topic = model.Topic,
                Explanation = model.Explanation,
                UserId = model.UserId
            };

            return await _questionRepository.CreateQuestion(question);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Question>> Get(long questionId)
    {
        try
        {
            return await _questionRepository.Get(questionId);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<IEnumerable<Question>>> Get()
    {
        try
        {
            return await _questionRepository.Get();
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Question>> UpdateQuestion(QuestionVM model)
    {
        try
        {
            var question = new Question {
                QuestionId = model.QuestionId,
                Topic = model.Topic,
                Explanation = model.Explanation,
                UserId = model.UserId
            };

            return await _questionRepository.Update(question);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<long>> DeleteQuestion(long questionId)
    {
        try
        {
            return await _questionRepository.Delete(questionId);
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }
}
