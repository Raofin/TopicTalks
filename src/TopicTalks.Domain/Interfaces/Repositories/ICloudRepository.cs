using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Repositories;

public interface ICloudRepository : IRepository<CloudFile>
{
    Task<CloudFile> GetAsync(string cloudFileId);
}