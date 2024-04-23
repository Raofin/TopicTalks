using TopicTalks.Domain.Common;

namespace TopicTalks.Domain.Interfaces.Core;

public interface IGoogleCloud
{
    Task<GoogleFile> UploadAsync(string fileName, Stream stream, string contentType);
    Task<GoogleFile> InfoAsync(string fileId);
    Task<GoogleFileDownload> DownloadAsync(string fileId);
    Task<GoogleFile> UpdateAsync(string fileId, string fileName, Stream stream, string contentType);
    void Delete(string fileId);
}