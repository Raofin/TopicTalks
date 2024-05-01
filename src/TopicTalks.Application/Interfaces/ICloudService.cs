using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Common;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface ICloudService
{
    Task<CloudFile> UploadAsync(FileUploadDto dto, long? userId = null, bool commit = true);
    Task<CloudFile?> InfoAsync(string fileId);
    Task<GoogleFileDownload> DownloadAsync(string fileId);
    Task<CloudFile> UpdateAsync(string fileId, FileUploadDto dto, long? userId = null, bool commit = true);
    Task DeleteAsync(string fileId);
}