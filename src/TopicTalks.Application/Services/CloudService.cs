using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain;
using TopicTalks.Domain.Common;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class CloudService(IGoogleCloud googleCloud, IUnitOfWork unitOfWork) : ICloudService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IGoogleCloud _googleCloud = googleCloud;

    public async Task<CloudFileDto> UploadAsync(string fileName, Stream stream, string contentType, long? userId = null)
    {
        var googleFile = await _googleCloud.UploadAsync(fileName, stream, contentType);
        var cloudFile = MapToCloudFile(googleFile, userId);

        _unitOfWork.CloudFile.Add(cloudFile);
        await _unitOfWork.CommitAsync();

        return MapToDto(cloudFile);
    }

    public async Task<CloudFileDto?> InfoAsync(string fileId)
    {
        var cloudFile = await _unitOfWork.CloudFile.SingleOrDefaultAsync(x => x.CloudFileId == fileId);

        return cloudFile is null ? null : MapToDto(cloudFile);
    }

    public async Task<GoogleFileDownload> DownloadAsync(string fileId)
    {
        return await _googleCloud.DownloadAsync(fileId);
    }

    public async Task<CloudFileDto> UpdateAsync(string fileId, string fileName, Stream stream, string contentType, long? userId = null)
    {
        var updatedFile = await _googleCloud.UpdateAsync(fileId, fileName, stream, contentType);
        var cloudFile = MapToCloudFile(updatedFile, userId);

        _unitOfWork.CloudFile.Remove(new CloudFile { CloudFileId = fileId });
        _unitOfWork.CloudFile.Add(cloudFile);
        await _unitOfWork.CommitAsync();

        return MapToDto(cloudFile);
    }

    public async Task DeleteAsync(string fileId)
    {
        _googleCloud.Delete(fileId);

        _unitOfWork.CloudFile.Remove(new CloudFile { CloudFileId = fileId });
        await _unitOfWork.CommitAsync();
    }

    #region ### Map Methods ###

    private static CloudFile MapToCloudFile(GoogleFile cloudFileInfo, long? userId = null)
    {
        return new CloudFile {
            CloudFileId = cloudFileInfo.CloudFileId,
            Name = cloudFileInfo.Name,
            ContentType = cloudFileInfo.ContentType,
            Size = cloudFileInfo.Size,
            WebContentLink = cloudFileInfo.WebContentLink,
            WebViewLink = cloudFileInfo.WebViewLink,
            DirectLink = cloudFileInfo.DirectLink,
            CreatedAt = cloudFileInfo.CreatedAt,
            UserId = userId
        };
    }

    private static CloudFileDto MapToDto(CloudFile cloudFile)
    {
        return new CloudFileDto(
            cloudFile.CloudFileId,
            cloudFile.Name,
            cloudFile.ContentType,
            cloudFile.Size,
            cloudFile.WebContentLink,
            cloudFile.WebViewLink,
            cloudFile.DirectLink,
            cloudFile.CreatedAt,
            cloudFile.UserId
        );
    }

    #endregion
}