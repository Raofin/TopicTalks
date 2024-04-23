using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Common;

namespace TopicTalks.Application.Interfaces;

public interface ICloudService
{
    Task<CloudFileDto> UploadAsync(string fileName, Stream stream, string contentType, long? userId = null);
    Task<CloudFileDto?> InfoAsync(string fileId);
    Task<GoogleFileDownload> DownloadAsync(string fileId);
    Task<CloudFileDto> UpdateAsync(string fileId, string fileName, Stream stream, string contentType, long? userId = null);
    Task DeleteAsync(string fileId);
}