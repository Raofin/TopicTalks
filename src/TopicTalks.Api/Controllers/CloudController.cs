using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Api.Controllers;

[AllowAnonymous]
public class CloudController(IGoogleCloud googleCloud) : ApiController
{
    private readonly IGoogleCloud _googleCloud = googleCloud;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileInfo = await _googleCloud.UploadAsync(file.FileName, stream, file.ContentType);

        return Ok(fileInfo);
    }

    [HttpGet("info/{fileId}")]
    public async Task<IActionResult> GetFileUrl(string fileId)
    {
        var fileInfo = await _googleCloud.InfoAsync(fileId);
        return Ok(fileInfo);
    }

    [HttpGet("{fileId}")]
    public async Task<IActionResult> DownloadFile(string fileId)
    {
        var fileBytes = await _googleCloud.DownloadAsync(fileId);

        return File(fileBytes, "image/jpeg");
    }


    [HttpDelete("{fileId}")]
    public IActionResult DeleteFile(string fileId)
    {
        _googleCloud.Delete(fileId);
        return Ok();
    }

    [HttpPut("{fileId}")]
    public async Task<IActionResult> UpdateFile(string fileId, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileInfo = await _googleCloud.UpdateAsync(fileId, file.FileName, stream, file.ContentType);
        return Ok(fileInfo);
    }
}