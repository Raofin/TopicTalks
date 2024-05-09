using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

public class WwwootService(IHostEnvironment hostEnvironment) : IWwwootService
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;

    public string GetPath(params string[] paths)
    {
        var basePaths = new[] { _hostEnvironment.ContentRootPath, "wwwroot"};
        return Path.Combine(basePaths.Concat(paths).ToArray());
    }
    
    public byte[] GetBytes(params string[] paths)
    {
        return File.ReadAllBytes(GetPath(paths));
    }
    
    public string GetDataUri(params string[] paths)
    {
        var mimeType = GetMimeTypeForFileExtension(GetPath(paths));
        var base64 = Convert.ToBase64String(GetBytes(paths));
        return $"data:{mimeType};base64,{base64}";
    }

    private static string GetMimeTypeForFileExtension(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"The file {filePath} does not exist.");
        }

        var extension = Path.GetExtension(filePath).ToLowerInvariant();

        var provider = new FileExtensionContentTypeProvider();

        return provider.TryGetContentType(extension, out var contentType) 
            ? contentType : "application/octet-stream";
    }
}

