using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Infrastructure.Services.Cloud;

namespace TopicTalks.Api.Controllers;

[AllowAnonymous]
public class CloudController(IGoogleCloud googleCloud) : ApiController
{
    private readonly IGoogleCloud _googleCloud = googleCloud;

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileName = file.FileName;
        var contentType = file.ContentType;
        var fileId = await _googleCloud.UploadFileAsync(stream, fileName, contentType);

        return Ok(fileId);
    }

    [HttpGet("url/{fileId}")]
    public async Task<IActionResult> GetFileUrl(string fileId)
    {
        var fileUrl = await _googleCloud.GetFileContentUrlAsync(fileId);
        return Ok(new { FileUrl = fileUrl });
    }

    [HttpGet("download/{fileId}")]
    public async Task<IActionResult> DownloadFile(string fileId)
    {
        var fileBytes = await _googleCloud.DownloadFileAsync(fileId);

        return File(fileBytes, "image/jpeg");
    }


    [HttpDelete("delete/{fileId}")]
    public async Task<IActionResult> DeleteFile(string fileId)
    {
        await _googleCloud.DeleteFileAsync(fileId);
        return Ok();
    }

    [HttpPut("update/{fileId}")]
    public async Task<IActionResult> UpdateFile(string fileId, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileName = file.FileName;
        var contentType = file.ContentType;
        var updatedFileId = await _googleCloud.UpdateFileAsync(fileId, stream, fileName, contentType);
        return Ok(updatedFileId);
    }
}