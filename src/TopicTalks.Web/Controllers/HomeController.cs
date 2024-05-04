using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Common;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;
using TopicTalks.Web.ViewModels;
using ILogger = Serilog.ILogger;

namespace TopicTalks.Web.Controllers;

[Authorize]
public class HomeController(IHttpService httpService, ILogger logger) : Controller
{
    private readonly IHttpService _httpService = httpService;
    private readonly ILogger _logger = logger;

    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> Dashboard(string? searchQuery)
    {
        for (var i = 0; i < 3; i++)
        {
            try
            {
                var response = await _httpService.Client.GetAsync($"api/question?searchQuery={searchQuery}");

                return response.IsSuccessStatusCode
                    ? View(response.DeserializeTo<List<QuestionViewModel>>())
                    : RedirectToAction("Error");
            }
            catch (HttpRequestException ex) when (ex.InnerException is System.Net.Sockets.SocketException)
            {
                _logger.Warning("Error connecting to the API. Retrying in 5 seconds...");
                await Task.Delay(5000);
            }
        }
        
        throw new HttpRequestException("Error connecting to the API. Please try again later.");
    }

    [HttpGet("question")]
    public IActionResult PostQuestion()
    {
        return View();
    }

    [AuthorizeStudent]
    [HttpGet("my-questions")]
    public async Task<IActionResult> UserQuestions()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/questions");
        
        return response.IsSuccessStatusCode
            ? View(response.DeserializeTo<List<QuestionViewModel>>())
            : RedirectToAction("Error");
    }

    [AuthorizeTeacher]
    [HttpGet("my-responded-questions")]
    public async Task<IActionResult> UserResponses()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/responses");

        return response.IsSuccessStatusCode
            ? View(response.DeserializeTo<List<QuestionViewModel>>())
            : RedirectToAction("Error");
    }

    [AllowAnonymous]
    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> QuestionDetails(long questionId)
    {
        var response = await _httpService.Client.GetAsync($"api/question/withAnswers/{questionId}");

        return response.IsSuccessStatusCode
            ? View(response.DeserializeTo<QuestionWithAnswersViewModel>())
            : RedirectToAction("Error");
    }

    [HttpGet("question/LoadQuestionList")]
    public async Task<IActionResult> LoadQuestionList(List<QuestionViewModel> questions, string? searchQuery)
    {
        if (questions.Count > 0)
        {
            return PartialView("~/Views/Partials/_QuestionList.cshtml", questions);
        }

        var response = await _httpService.Client.GetAsync($"api/question?searchQuery={searchQuery}");

        return response.IsSuccessStatusCode
            ? PartialView("~/Views/Partials/_QuestionList.cshtml", response.DeserializeTo<List<QuestionViewModel>>())
            : new StatusCodeResult((int)response.StatusCode);
    }

    [AllowAnonymous]
    [HttpGet("error")]
    public IActionResult Error()
    {
        return View();
    }
}
