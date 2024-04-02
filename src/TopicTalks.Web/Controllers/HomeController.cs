using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TopicTalks.Web.Attributes;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers;

public class HomeController(IHttpService httpService) : Controller
{
    private readonly IHttpService _httpService = httpService;

    [HttpGet("")]
    public async Task<IActionResult> Dashboard()
    {
        var response = await _httpService.Client.GetAsync("api/question");

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
    [HttpGet("my-responses")]
    public async Task<IActionResult> MyResponses()
    {
        var response = await _httpService.Client.GetAsync("api/answer/currentUser/answers");

        var answers = JsonConvert.DeserializeObject<List<QuestionViewModel>>(response.ToJson())!;

        return View(answers);
    }

    [Authorize]
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

    [HttpPost("post-question")]
    public async Task<IActionResult> PostQuestion(QuestionCreateViewModel request)
    {
        var response = await _httpService.Client.PostAsync("api/question", request.ToStringContent());

        return response.IsSuccessStatusCode
            ? Ok(response.DeserializeTo<QuestionWithAnswersViewModel>())
            : response.StatusCode switch {
                HttpStatusCode.Unauthorized => Unauthorized(),
                _ => Problem()
            };
    }
}
