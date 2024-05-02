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
        var uploadedFile = JsonConvert.DeserializeObject<CloudFileViewModel>(response.ToJson())!;

        return response.IsSuccessStatusCode 
            ? Ok(uploadedFile) 
            : StatusCode((int)response.StatusCode, "Error uploading file.");
    }

    [Authorize]
    [HttpPut("changeProfileImage")]
    public async Task<IActionResult> ChangeProfileImage(IFormFile file)
    {
        using var content = new MultipartFormDataContent();
        content.Add(new StreamContent(file.OpenReadStream()), "file", file.FileName);

        var response = await _httpService.Client.PutAsync("api/account/profileImage", content);
        var uploadedFile = JsonConvert.DeserializeObject<CloudFileViewModel>(response.ToJson())!;

        var cookieValue = _httpAccessor.HttpContext!.Request.Cookies["UserInfo"]!;
        var userInfo = JsonConvert.DeserializeObject<UserInfoCookies>(cookieValue)!;
        userInfo.ImageFile = uploadedFile;

        _httpAccessor.HttpContext.Response.Cookies.Append(
            "UserInfo",
            JsonConvert.SerializeObject(userInfo), 
            new CookieOptions { HttpOnly = true }
        );

        return response.IsSuccessStatusCode
            ? Ok(uploadedFile)
            : StatusCode((int)response.StatusCode, "Error uploading file.");
    }
}