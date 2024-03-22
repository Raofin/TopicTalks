using ErrorOr;
using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces;
using TopicTalks.Domain.Models;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class QuestionRepository(TopicTalksDbContext _dbContext) : IQuestionRepository
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
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<List<QuestionModel>>> Get(string? searchText)
    {
        try
        {
            var searchTextLower = searchText?.ToLower();

            var questions = await _dbContext.Questions
                .Include(q => q.User)
                .OrderByDescending(q => q.CreatedAt)
                .Where(q => string.IsNullOrEmpty(searchTextLower)
                         || q.Topic.ToLower().Contains(searchTextLower)
                         || q.Explanation.ToLower().Contains(searchTextLower))
                .Select(q => new QuestionModel {
                    Question = new Question {
                        QuestionId = q.QuestionId,
                        Topic = q.Topic,
                        Explanation = q.Explanation,
                        UserId = q.UserId,
                        CreatedAt = q.CreatedAt,
                        UpdatedAt = q.UpdatedAt,
                        User = q.User,
                    },
                    HasTeachersResponse = _dbContext.Answers
                        .Where(a => a.QuestionId == q.QuestionId)
                        .Any(a => a.User != null &&
                                    a.User.UserRoles != null &&
                                    a.User.UserRoles.Any(ur => ur.Role != null && ur.Role.RoleName == "Teacher"))
                })
                .AsNoTracking()
                .ToListAsync();

            return questions;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
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
                return Error.NotFound();
            }

            return question;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<List<Question>>> GetMyQuestions(long userId)
    {
        try
        {
            var questions = await _dbContext.Questions
                            .Include(q => q.User)
                            .Where(q => q.User != null && q.User.UserId == userId)
                            .ToListAsync();

            return questions;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<List<Question>>> GetMyRespondedQuestions(long userId)
    {
        try
        {
            var questions = await (
                from answer in _dbContext.Answers
                where answer.UserId == userId
                join question in _dbContext.Questions.Include(q => q.User)
                    on answer.QuestionId equals question.QuestionId
                select question
            ).ToListAsync();

            return questions;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<Question>> Update(Question updatedQuestion)
    {
        try
        {
            var existingQuestion = await _dbContext.Questions.FindAsync(updatedQuestion.QuestionId);

            if (existingQuestion == null)
            {
                return Error.NotFound();
            }

            existingQuestion.Topic = updatedQuestion.Topic ?? existingQuestion.Topic;
            existingQuestion.Explanation = updatedQuestion.Explanation ?? existingQuestion.Explanation;
            existingQuestion.UpdatedAt = DateTime.Now;

            await _dbContext.SaveChangesAsync();

            return existingQuestion;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }

    public async Task<ErrorOr<long>> Delete(long questionId)
    {
        try
        {
            var questionToDelete = await _dbContext.Questions.FindAsync(questionId);

            if (questionToDelete == null)
            {
                return Error.NotFound();
            }

            _dbContext.Questions.Remove(questionToDelete);
            await _dbContext.SaveChangesAsync();

            return questionId;
        }
        catch (Exception ex)
        {
            return Error.Unexpected(description: ex.Message);
        }
    }
}