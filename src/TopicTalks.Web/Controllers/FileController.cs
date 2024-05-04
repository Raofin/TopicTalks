using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

[Route("File")]
public class FileController(IHttpService httpService, IHttpContextAccessor httpContextAccessor) : Controller
{
    private readonly IHttpContextAccessor _httpAccessor = httpContextAccessor;
    private readonly IHttpService _httpService = httpService;

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

        var response = await _httpService.Client.PostAsync("api/cloud", content);

        return response.IsSuccessStatusCode
            ? Ok(JsonConvert.DeserializeObject<CloudFileViewModel>(response.ToJson())!)
            : StatusCode((int)response.StatusCode, "Error uploading file.");
    }

    [Authorize]
    [HttpPut("changeProfileImage")]
    public async Task<IActionResult> ChangeProfileImage(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);
        var response = await _httpService.Client.PutAsync("api/account/profileImage", content);

        if (!response.IsSuccessStatusCode)
        {
            return StatusCode((int)response.StatusCode, "Error uploading file.");
        }

        var uploadedFile = JsonConvert.DeserializeObject<CloudFileViewModel>(response.ToJson())!;
        var cookieValue = _httpAccessor.HttpContext!.Request.Cookies["UserInfo"]!;
        var userInfo = JsonConvert.DeserializeObject<dynamic>(cookieValue)!;

        userInfo.ImageFile = uploadedFile;

        _httpAccessor.HttpContext.Response.Cookies.Append(
            "UserInfo",
            JsonConvert.SerializeObject(userInfo),
            new CookieOptions {
                HttpOnly = true,
                Expires = DateTimeOffset.UtcNow.AddDays(7)
            }
        );

        return Ok(uploadedFile);
    }
}