using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Repositories;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class QuestionRepository(AppDbContext dbContext) : Repository<Question>(dbContext), IQuestionRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<List<Question>> SearchAsync(string? searchText)
    {
        var searchTextLower = searchText?.ToLower();

        var questions = await _dbContext.Questions
            .Include(q => q.User)
            .OrderByDescending(q => q.CreatedAt)
            .Where(q => string.IsNullOrEmpty(searchTextLower)
                        || q.Topic.ToLower().Contains(searchTextLower)
                        || q.Explanation.ToLower().Contains(searchTextLower))
            .Select(q => new Question
            {
                QuestionId = q.QuestionId,
                Topic = q.Topic,
                Explanation = q.Explanation,
                UserId = q.UserId,
                CreatedAt = q.CreatedAt,
                UpdatedAt = q.UpdatedAt,
                User = q.User,
                HasTeachersResponse = _dbContext.Answers
                    .Where(a => a.QuestionId == q.QuestionId)
                    .Any(a => a.User != null && a.User.UserRoles
                        .Any(ur => ur.Role.RoleName == nameof(RoleType.Teacher)))
            })
            .AsNoTracking()
            .ToListAsync();

        return questions;
    }

    public async Task<List<Question>> GetWithUserAsync()
    {
        var questions = await _dbContext.Questions
            .Include(q => q.User)
            .ToListAsync();

        return questions;
    }

    public async Task<Question?> GetWithUserAsync(long questionId)
    {
        var question = await _dbContext.Questions
            .Include(q => q.User)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);

        return question;
    }

    public async Task<List<Question>> GetByUserIdAsync(long userId)
    {
        var questions = await _dbContext.Questions
            .Include(q => q.User)
            .Where(q => q.User != null && q.User.UserId == userId)
            .ToListAsync();

        return questions;
    }

    public async Task<List<Question>> GetByUserResponsesAsync(long userId)
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

    public async Task<Question?> GetWithAnswersAsync(long questionId)
    {
        var question = await _dbContext.Questions
            .Include(a => a.User)
            .Include(q => q.Answers)
            .ThenInclude(a => a.User)
            .ThenInclude(u => u!.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Where(q => q.QuestionId == questionId)
            .AsSingleQuery()
            .SingleOrDefaultAsync();

        return question;
    }
}