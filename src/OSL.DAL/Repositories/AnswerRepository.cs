using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

namespace OSL.DAL.Repositories;

internal class AnswerRepository(OslDbContext _dbContext) : IAnswerRepository
{
    public async Task<ErrorOr<Answer>> Create(Answer answer)
    {
        try
        {
            _dbContext.Answers.Add(answer);
            await _dbContext.SaveChangesAsync();

            answer.User = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserId == answer.UserId);

            return answer;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Answer>> Get(long answerId)
    {
        try
        {
            var answer = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.AnswerId == answerId)
                .Select(a => new Answer {
                    AnswerId = a.AnswerId,
                    ParentAnswerId = a.ParentAnswerId,
                    QuestionId = a.QuestionId,
                    Explanation = a.Explanation,
                    UserId = a.UserId,
                    CreatedAt = a.CreatedAt,
                    User = a.User
                })
                .FirstOrDefaultAsync();

            if (answer == null)
            {
                return Error.NotFound();
            }

            return answer;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<List<Answer>>> Get(long questionId, long parentAnswerId)
    {
        try
        {
            var answers = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.QuestionId == questionId && a.ParentAnswerId == parentAnswerId)
                .ToListAsync();

            return answers;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Answer>> Get(long questionId, long answerId, long parentAnswerId)
    {
        try
        {
            var answer = await _dbContext.Answers
                            .Include(a => a.User)
                            .FirstOrDefaultAsync(a => a.QuestionId == questionId
                                && a.AnswerId == answerId
                                && a.ParentAnswerId == parentAnswerId);

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
            var answersToDelete = await _dbContext.Answers
                .Where(a => a.AnswerId == answerId || a.ParentAnswerId == answerId)
                .ToListAsync();

            if (answersToDelete.Count == 0)
            {
                return Error.NotFound("Answer not found.");
            }

            _dbContext.RemoveRange(answersToDelete);
            await _dbContext.SaveChangesAsync();

            return answerId;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<bool>> HasTeachersAnswer(int questionId)
    {
        try
        {
            var hasTeachersAnswer = await _dbContext.Answers
                .Where(a => a.QuestionId == questionId)
                .AnyAsync(a => a.User != null &&
                          a.User.UserRoles != null &&
                          a.User.UserRoles.Any(ur => ur.Role != null && ur.Role.RoleName == "Teacher"));

            return hasTeachersAnswer;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}