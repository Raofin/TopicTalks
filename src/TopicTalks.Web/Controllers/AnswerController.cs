using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

public class AnswerController(IHttpService httpService) : Controller
{
    private readonly IHttpService _httpService = httpService;

    [HttpPost]
    public async Task<IActionResult> PostAnswer(AnswerRequestViewModel request)
    {
        var response = await _httpService.Client.PostAsync("api/answer", request.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<AnswerViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateAnswer(AnswerRequestViewModel request)
    {
        var response = await _httpService.Client.PatchAsync("api/answer", request.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<AnswerViewModel>())
            : new StatusCodeResult((int)response.StatusCode);
    }
}