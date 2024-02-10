using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.DAL.Repositories;

internal class AnswerRepository(OslDbContext _dbContext) : IAnswerRepository
{
    public async Task<ErrorOr<Answer>> Create(Answer answer)
    {
        try
        {
            _dbContext.Answers.Add(answer);
            await _dbContext.SaveChangesAsync();

            return answer;
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
            var answers = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.QuestionId == questionId)
                //.OrderByDescending(a => a.AnswerId)
                .Select(a => new Answer {
                    AnswerId = a.AnswerId,
                    ParentAnswerId = a.ParentAnswerId,
                    QuestionId = a.QuestionId,
                    Explanation = a.Explanation,
                    UserId = a.UserId,
                    CreatedAt = a.CreatedAt,
                    User = a.User
                })
                .ToListAsync();

            return answers;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Answer>> Get(long questionId, long answerId)
    {
        try
        {
            var answer = await _dbContext.Answers
                            .Include(a => a.User)
                            .FirstOrDefaultAsync(a => a.QuestionId == questionId && a.AnswerId == answerId);

            if (answer == null)
            {
                return Error.NotFound("Answer not found.");
            }

            return answer;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Answer>> Update(Answer updatedAnswer)
    {
        try
        {
            var existingAnswer = await _dbContext.Answers.FindAsync(updatedAnswer.AnswerId);

            if (existingAnswer == null)
            {
                return Error.NotFound("Answer not found.");
            }

            existingAnswer.Explanation = updatedAnswer.Explanation;

            await _dbContext.SaveChangesAsync();

            return existingAnswer;
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
            var answerToDelete = await _dbContext.Answers.FindAsync(answerId);

            if (answerToDelete == null)
            {
                return Error.NotFound("Answer not found.");
            }

            _dbContext.Answers.Remove(answerToDelete);
            await _dbContext.SaveChangesAsync();

            return answerId;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }
}
