using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[AllowAnonymous]
public class CloudController(ICloudService cloudService) : ApiController
{
    private readonly ICloudService _cloudService = cloudService;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileInfo = await _cloudService.UploadAsync(file.FileName, stream, file.ContentType/*, User.GetUserId()*/);

        return Ok(fileInfo);
    }

    [HttpGet("info/{fileId}")]
    public async Task<IActionResult> GetFileUrl(string fileId)
    {
        var fileInfo = await _cloudService.InfoAsync(fileId);

        return Ok(fileInfo);
    }

    [AllowAnonymous]
    [HttpGet("{fileId}")]
    public async Task<IActionResult> DownloadFile(string fileId)
    {
        var file = await _cloudService.DownloadAsync(fileId);

        return File(file.Bytes, file.ContentType, file.Name);
    }

    [HttpDelete("{fileId}")]
    public async Task<IActionResult> DeleteFile(string fileId)
    {
        await _cloudService.DeleteAsync(fileId);

        return Ok();
    }

    [HttpPut("{fileId}")]
    public async Task<IActionResult> UpdateFile(string fileId, IFormFile file)
    {
        await using var stream = file.OpenReadStream();
        var fileInfo = await _cloudService.UpdateAsync(fileId, file.FileName, stream, file.ContentType/*, User.GetUserId()*/);

        return Ok(fileInfo);
    }
}