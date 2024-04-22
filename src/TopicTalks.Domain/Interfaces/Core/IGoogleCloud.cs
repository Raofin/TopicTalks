using TopicTalks.Domain.Common;

namespace TopicTalks.Domain.Interfaces.Core;

public interface IGoogleCloud
{
    Task<CloudFile> UploadAsync(string fileName, Stream stream, string contentType);
    Task<CloudFile> InfoAsync(string fileId);
    Task<byte[]> DownloadAsync(string fileId);
    Task<CloudFile> UpdateAsync(string fileId, string fileName, Stream stream, string contentType);
    void Delete(string fileId);
}