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

        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .Include(q => q.ImageFile)
            .OrderByDescending(q => q.CreatedAt)
            .Where(q => string.IsNullOrEmpty(searchTextLower)
                        || q.Topic.ToLower().Contains(searchTextLower)
                        || q.Explanation.ToLower().Contains(searchTextLower))
            .Select(q => new Question {
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
                        .Any(ur => ur.Role.RoleName == nameof(RoleType.Teacher))),
                ImageFile = q.ImageFile
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<List<Question>> GetWithUserAsync()
    {
        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .ToListAsync();
    }

    public async Task<Question?> GetWithUserAsync(long questionId)
    {
        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .Include(q => q.ImageFile)
            .FirstOrDefaultAsync(q => q.QuestionId == questionId);
    }

    public async Task<List<Question>> GetByUserIdAsync(long userId)
    {
        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .Where(q => q.User != null && q.User.UserId == userId)
            .ToListAsync();
    }

    public async Task<Question?> GetByUserIdAsync(long questionId, long userId)
    {
        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .Where(q => q.QuestionId == questionId && q.User != null && q.User.UserId == userId)
            .SingleOrDefaultAsync();
    }

    public async Task<List<Question>> GetByUserResponsesAsync(long userId)
    {
        return await _dbContext.Questions
            .Include(q => q.User)
            .ThenInclude(u => u!.ImageFile)
            .Include(q => q.ImageFile)
            .Where(q => q.Answers.Any(a => a.UserId == userId))
            .ToListAsync();
    }

    public async Task<Question?> GetWithAnswersAsync(long questionId)
    {
        return await _dbContext.Questions
            .Include(a => a.User)
            .ThenInclude(u => u!.ImageFile)
            .Include(q => q.Answers)
            .ThenInclude(a => a.User)
            .ThenInclude(u => u!.UserRoles)
            .ThenInclude(ur => ur.Role)
            .Include(q => q.ImageFile)
            .Where(q => q.QuestionId == questionId)
            .SingleOrDefaultAsync();
    }
}