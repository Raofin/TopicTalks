using Microsoft.EntityFrameworkCore;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Repositories;

namespace TopicTalks.Infrastructure.Persistence.Repositories;

internal class CloudRepository(AppDbContext dbContext) : Repository<CloudFile>(dbContext), ICloudRepository
{
    private readonly AppDbContext _dbContext = dbContext;

    public async Task<CloudFile> GetAsync(string cloudFileId)
    {
        return await _dbContext.CloudFiles
            .Include(c => c.User)
            .Where(c => c.CloudFileId == cloudFileId)
            .SingleAsync();
    }
}