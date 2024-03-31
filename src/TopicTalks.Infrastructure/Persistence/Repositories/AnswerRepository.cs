﻿using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class AnswerRepository(AppDbContext dbContext) : Repository<Answer>(dbContext), IAnswerRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<Answer?> GetWithUserAsync(long answerId)
    {
        var answer = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.AnswerId == answerId)
                .FirstOrDefaultAsync();

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

    public async Task<Answer?> GetRepliesAsync(long questionId, long answerId, long parentAnswerId)
    {
        var answer = await _dbContext.Answers
                .Include(a => a.User)
                .Where(a => a.QuestionId == questionId
                    && a.AnswerId == answerId
                    && a.ParentAnswerId == parentAnswerId)
                .FirstOrDefaultAsync();

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

    public void Update(Answer answer)
    {
        _dbContext.Answers.Update(answer);
    }
}