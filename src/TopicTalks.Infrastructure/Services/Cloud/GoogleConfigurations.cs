using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

namespace TopicTalks.Infrastructure.Services.Cloud;

public class GoogleConfigurations(IMemoryCache memoryCache, GoogleCloudSettings googleCloudOptions, ILogger logger)
{
    private readonly IMemoryCache _memoryCache = memoryCache;
    private readonly GoogleCloudSettings _settings = googleCloudOptions;
    private readonly ILogger _logger = logger;

    public DriveService GetDriveService()
    {
        if (!File.Exists(_settings.Credentials) || File.ReadAllLines(_settings.Credentials).Length < 10)
        {
            _logger.Error("Google credentials file not found or invalid.");
            return new DriveService();
        }

        var credential = GoogleCredential.FromFile(_settings.Credentials)
            .CreateScoped(DriveService.Scope.Drive);

        var driveService = new DriveService(
            new BaseClientService.Initializer {
                HttpClientInitializer = credential,
                ApplicationName = "TopicTalks",
            });

        SetFolderId(driveService);

        return driveService;
    }

    public void SetFolderId(DriveService driveService)
    {
        const string cacheKey = "TT_FolderId";

        var request = driveService.Files.List();
        request.Q = $"name='{_settings.FolderName}'";
        request.Fields = "files(id)";

        var files = request.Execute();

        if (files.Files.Count > 0)
        {
            var fileId = files.Files.FirstOrDefault()?.Id;
            _memoryCache.Set(cacheKey, fileId);
        }
    }
}