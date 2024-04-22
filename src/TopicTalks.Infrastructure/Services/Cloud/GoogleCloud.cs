using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Upload;
using Microsoft.Extensions.Caching.Memory;
using TopicTalks.Domain.Common;
using TopicTalks.Domain.Interfaces.Core;
using File = Google.Apis.Drive.v3.Data.File;

namespace TopicTalks.Infrastructure.Services.Cloud;

internal class GoogleCloud(IMemoryCache cache, DriveService driveService) : IGoogleCloud
{
    private readonly IMemoryCache _cache = cache;
    private readonly DriveService _driveService = driveService;

    public async Task<CloudFile> UploadAsync(string fileName, Stream stream, string contentType)
    {
        var metaData = CreateMetaData(fileName);
        var request = _driveService.Files.Create(metaData, stream, contentType);

        request.Fields = "id, name, mimeType, size, webContentLink, webViewLink, createdTime";
        var result = await request.UploadAsync();

        if (result.Status == UploadStatus.Failed)
            throw new Exception("Failed to upload file");

        SetPermissions(request.ResponseBody.Id);

        return ToCloudFile(request.ResponseBody);
    }

    private void SetPermissions(string fileId)
    {
        var permission = new Permission {
            Type = "anyone",
            Role = "reader"
        };

        _driveService.Permissions
            .Create(permission, fileId)
            .ExecuteAsync();
    }

    public async Task<CloudFile> InfoAsync(string fileId)
    {
        var request = _driveService.Files.Get(fileId);
        request.Fields = "id, name, mimeType, size, webContentLink, webViewLink, createdTime";
        var response = await request.ExecuteAsync();

        return ToCloudFile(response);
    }

    public async Task<byte[]> DownloadAsync(string fileId)
    {
        var request = _driveService.Files.Get(fileId);
        request.Fields = "id, name, mimeType, size, webContentLink, webViewLink, createdTime";

        using var stream = new MemoryStream();
        await request.DownloadAsync(stream);

        return stream.ToArray();
    }

    public async Task<CloudFile> UpdateAsync(string fileId, string fileName, Stream stream, string contentType)
    {
        Delete(fileId);
        return await UploadAsync(fileName, stream, contentType);
    }

    public void Delete(string fileId)
    {
        _driveService.Files.Delete(fileId).ExecuteAsync();
    }

    private File CreateMetaData(string fileName)
    {
        const string cacheKey = "TT_FolderId";
        var metaData = new File { Name = fileName, };

        if (_cache.TryGetValue(cacheKey, out string? fileId) && fileId is not null)
        {
            metaData.Parents = new List<string> { fileId };
        }

        return metaData;
    }

    private static string GetDirectLink(string fileId) => "https://lh3.googleusercontent.com/d/" + fileId;

    private static CloudFile ToCloudFile(File file)
    {
        return new CloudFile(
            file.Id,
            file.Name,
            file.MimeType,
            file.Size ?? 0,
            file.WebContentLink,
            file.WebViewLink,
            GetDirectLink(file.Id),
            file.CreatedTimeDateTimeOffset?.UtcDateTime ?? DateTime.Now
        );
    }
}