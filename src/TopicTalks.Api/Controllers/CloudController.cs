using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

[AllowAnonymous]
public class CloudController(ICloudService cloudService, IValidator<IFormFile> validator) : ApiController
{
    private readonly ICloudService _cloudService = cloudService;
    private readonly IValidator<IFormFile> _validator = validator;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        var validationResult = await _validator.ValidateAsync(file);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.ToDto());
        }

        long? userId = User.Identity is { IsAuthenticated: true } ? User.GetUserId() : null;

        await using var stream = file.OpenReadStream();
        var fileInfo = await _cloudService.UploadAsync(new FileUploadDto(file.FileName, stream, file.ContentType), userId);

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
        long? userId = User.Identity is { IsAuthenticated: true } ? User.GetUserId() : null;
        
        await using var stream = file.OpenReadStream();
        var fileInfo = await _cloudService.UpdateAsync(fileId, new FileUploadDto(file.FileName, stream, file.ContentType), userId);

        return Ok(fileInfo);
    }
}