using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

[Authorize]
[Route("answer")]
public class AnswerController(IHttpService httpService) : Controller
{
    private readonly IHttpService _httpService = httpService;

    [HttpPost]
    public async Task<IActionResult> PostAnswer(AnswerCreateViewModel request)
    {
        var response = await _httpService.Client.PostAsync("api/answer", request.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<AnswerViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [HttpGet("{answerId}")]
    public async Task<IActionResult> GetAnswer(long answerId)
    {
        var response = await _httpService.Client.GetAsync($"api/answer/{answerId}");

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<AnswerViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAnswer(AnswerUpdateViewModel request)
    {
        var response = await _httpService.Client.PatchAsync("api/answer", request.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<AnswerViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [HttpDelete("{answerId}")]
    public async Task<IActionResult> DeleteAnswer(long answerId)
    {
        var response = await _httpService.Client.DeleteAsync($"api/answer/{answerId}");

        return response.IsSuccessStatusCode
            ? Ok()
            : new StatusCodeResult((int)response.StatusCode);
    }
}