using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TopicTalks.Web.Common;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services.Interfaces;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

[Authorize]
public class HomeController(IHttpService httpService) : Controller
{
    private readonly IHttpService _httpService = httpService;

    [AllowAnonymous]
    [HttpGet("")]
    public async Task<IActionResult> Dashboard(string? searchQuery)
    {
        var response = await _httpService.Client.GetAsync($"api/question?searchQuery={searchQuery}");

        var questions = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson())!;

        return View(questions);
    }

    [AuthorizeStudent]
    [HttpGet("my-questions")]
    public async Task<IActionResult> UserQuestions()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/questions");

        var questions = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson());

        return View(questions ?? []);
    }

    [AuthorizeTeacher]
    [HttpGet("my-responded-questions")]
    public async Task<IActionResult> UserResponses()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/responses");

        var answers = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson());

        return View(answers ?? []);
    }

    [AllowAnonymous]
    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> QuestionDetails(long questionId)
    {
        var response = await _httpService.Client.GetAsync($"api/question/withAnswers/{questionId}");

        var question = JsonConvert.DeserializeObject<QuestionWithAnswersViewModel>(response.ToJson())!;

        return View(question);
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

    [HttpGet("question")]
    public IActionResult PostQuestion()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpGet("error")]
    public IActionResult Error()
    {
        return View();
    }
}
