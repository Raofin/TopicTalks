using ErrorOr;
using Microsoft.EntityFrameworkCore;
using OSL.DAL.Entities;
using OSL.DAL.Interfaces;

namespace OSL.DAL.Repositories;

internal class QuestionRepository(OslDbContext _dbContext) : IQuestionRepository
{
    public async Task<ErrorOr<Question>> CreateQuestion(Question question)
    {
        try
        {
            _dbContext.Questions.Add(question);
            await _dbContext.SaveChangesAsync();

            return question;
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
            var questions = await _dbContext.Questions
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .Select(q => new Question {
                    QuestionId = q.QuestionId,
                    Topic = q.Topic,
                    Explanation = q.Explanation,
                    UserId = q.UserId,
                    CreatedAt = q.CreatedAt,
                    UpdatedAt = q.UpdatedAt,
                    User = q.User
                })
                .ToListAsync();

            return questions;
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
            var question = await _dbContext.Questions
                            .Include(q => q.User)
                            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

            if (question == null)
            {
                return Error.NotFound("Question not found.");
            }

            return question;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<Question>> Update(Question updatedQuestion)
    {
        try
        {
            var existingQuestion = await _dbContext.Questions.FindAsync(updatedQuestion.QuestionId);

            if (existingQuestion == null)
            {
                return Error.NotFound("Question not found.");
            }

            existingQuestion.Topic = updatedQuestion.Topic;
            existingQuestion.Explanation = updatedQuestion.Explanation;
            existingQuestion.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return existingQuestion;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }

    public async Task<ErrorOr<long>> Delete(long questionId)
    {
        try
        {
            var questionToDelete = await _dbContext.Questions.FindAsync(questionId);

            if (questionToDelete == null)
            {
                return Error.NotFound("Question not found.");
            }

            _dbContext.Questions.Remove(questionToDelete);
            await _dbContext.SaveChangesAsync();

            return questionId;
        }
        catch (Exception ex)
        {
            return Error.Failure($"Error: {ex.Message}");
        }
    }
}