using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TopicTalks.Web.Attributes;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
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
    public async Task<IActionResult> MyQuestions()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/questions");

        var questions = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson())!;

        return View(questions);
    }

    [AuthorizeTeacher]
    [HttpGet("my-responded-questions")]
    public async Task<IActionResult> MyResponses()
    {
        var response = await _httpService.Client.GetAsync("api/question/currentUser/responses");

        var answers = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson())!;

        return View(answers);
    }

    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> QuestionDetails(long questionId)
    {
        var response = await _httpService.Client.GetAsync($"api/question/withAnswers/{questionId}");

        var question = JsonConvert.DeserializeObject<QuestionWithAnswersViewModel>(response.ToJson())!;

        return View(question);
    }

    [HttpGet("post-question")]
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
