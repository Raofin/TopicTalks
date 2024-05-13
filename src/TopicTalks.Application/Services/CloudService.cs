using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
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

    public async Task<CloudFile> UploadAsync(FileUploadDto dto, long? userId = null, bool commit = true)
    {
        var googleFile = await _googleCloud.UploadAsync(dto.FileName, dto.Stream, dto.ContentType);
        var cloudFile = googleFile.ToCloudFile(userId);

        _unitOfWork.CloudFile.Add(cloudFile);

        if (commit) await _unitOfWork.CommitAsync();

        return cloudFile;
    }

    public async Task<CloudFile?> InfoAsync(string fileId)
    {
        return await _unitOfWork.CloudFile.SingleOrDefaultAsync(x => x.CloudFileId == fileId);
    }

    public async Task<GoogleFileDownload> DownloadAsync(string fileId)
    {
        return await _googleCloud.DownloadAsync(fileId);
    }

    public async Task<CloudFile> UpdateAsync(string fileId, FileUploadDto dto, long? userId = null, bool commit = true)
    {
        var updatedFile = await _googleCloud.UpdateAsync(fileId, dto.FileName, dto.Stream, dto.ContentType);
        var cloudFile = updatedFile.ToCloudFile(userId);

        _unitOfWork.CloudFile.Remove(new CloudFile { CloudFileId = fileId });
        _unitOfWork.CloudFile.Add(cloudFile);

        if (commit) await _unitOfWork.CommitAsync();

        return cloudFile;
    }

    public async Task DeleteAsync(string fileId)
    {
        _googleCloud.Delete(fileId);

        _unitOfWork.CloudFile.Remove(new CloudFile { CloudFileId = fileId });
        await _unitOfWork.CommitAsync();
    }
}