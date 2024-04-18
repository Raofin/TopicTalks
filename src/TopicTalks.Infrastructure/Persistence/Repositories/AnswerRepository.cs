using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Repositories;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class AnswerRepository(AppDbContext dbContext) : Repository<Answer>(dbContext), IAnswerRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Answer?> GetWithUserAsync(long answerId)
    {
        var answer = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.AnswerId == answerId)
                .SingleOrDefaultAsync();

        return answer;
    }

    public async Task<List<Answer>> GetParentAnswersAsync(long questionId, long parentAnswerId)
    {
        var answers = await _dbContext.Answers
            .Include(a => a.User)
            .Where(a => a.QuestionId == questionId && a.ParentAnswerId == parentAnswerId)
            .ToListAsync();

        return answers;
    }

    public async Task<List<Answer>> GetByQuestionAsync(long questionId)
    {
        var answers = await _dbContext.Answers
            .Include(a => a.User)
            .Where(a => a.QuestionId == questionId)
            .ToListAsync();

        return answers;
    }

    public async Task<Answer?> GetRepliesAsync(long questionId, long answerId, long parentAnswerId)
    {
        var answer = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.QuestionId == questionId
                    && a.AnswerId == answerId
                    && a.ParentAnswerId == parentAnswerId)
                .SingleOrDefaultAsync();

        return answer;
    }

    public async Task<bool> HasTeachersAnswerAsync(int questionId)
    {
        var hasTeachersAnswer = await _dbContext.Answers
                .Where(a => a.QuestionId == questionId
                    && a.User != null
                    && a.User.UserRoles.Any(
                        ur => ur.Role.RoleName == nameof(RoleType.Teacher)))
                .AnyAsync();

        return hasTeachersAnswer;
    }

    public async Task<bool> IsQuestionOrParentExists(long questionId, long? parentAnswerId)
    {
        var answerOrParentExists = await _dbContext.Answers.AnyAsync(
            a => a.AnswerId == parentAnswerId || !parentAnswerId.HasValue || parentAnswerId == 0);

        return answerOrParentExists || await _dbContext.Questions.AnyAsync(q => q.QuestionId == questionId);
    }
}