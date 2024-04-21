using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using File = Google.Apis.Drive.v3.Data.File;

namespace TopicTalks.Infrastructure.Services.Cloud;

public interface IGoogleCloud
{
    Task<string> UploadFileAsync(Stream stream, string fileName, string contentType);
    Task<string> GetFileContentUrlAsync(string fileId);
    string GetFileUrlAsync(string fileId);
    Task<byte[]> DownloadFileAsync(string fileId);
    Task DeleteFileAsync(string fileId);
    Task<string> UpdateFileAsync(string fileId, Stream stream, string fileName, string contentType);
}

internal class GoogleCloud(DriveService driveService) : IGoogleCloud
{
    private readonly DriveService _driveService = driveService;

    public async Task<string> UploadFileAsync(Stream stream, string fileName, string contentType)
    {
        var fileMetadata = new File {
            Name = fileName,
            Parents = new List<string> { await GetFileIdAsync("TopicTalks") }
        };

        var request = _driveService.Files.Create(fileMetadata, stream, contentType);
        request.Fields = "id";
        await request.UploadAsync();

        var permission = new Permission {
            Type = "anyone",
            Role = "reader"
        };

        await _driveService.Permissions
            .Create(permission, request.ResponseBody.Id)
            .ExecuteAsync();

        return request.ResponseBody.Id;
    }

    public async Task<string> GetFileContentUrlAsync(string fileId)
    {
        var request = _driveService.Files.Get(fileId);
        request.Fields = "webContentLink";
        var file = await request.ExecuteAsync();
        return file.WebContentLink;
    }

    public string GetFileUrlAsync(string fileId)
    {
        return "https://lh3.googleusercontent.com/d/" + fileId;
    }

    public async Task<byte[]> DownloadFileAsync(string fileId)
    {
        var request = _driveService.Files.Get(fileId);

        using var stream = new MemoryStream();
        await request.DownloadAsync(stream);
        var fileBytes = stream.ToArray();

        return fileBytes;
    }

    public async Task DeleteFileAsync(string fileId)
    {
        await _driveService.Files.Delete(fileId).ExecuteAsync();
    }

    public async Task<string> UpdateFileAsync(string fileId, Stream stream, string fileName, string contentType)
    {
        await DeleteFileAsync(fileId);
        return await UploadFileAsync(stream, fileName, contentType);
    }

    public async Task<string> GetFileIdAsync(string fileName)
    {
        var request = _driveService.Files.List();
        request.Q = $"name='{fileName}'";
        request.Fields = "files(id)";
        var files = await request.ExecuteAsync();

        return files.Files.First().Id;
    }
}