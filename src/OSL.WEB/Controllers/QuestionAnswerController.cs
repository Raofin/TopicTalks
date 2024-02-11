using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.DAL.Entities;
using OSL.WEB.Attributes;

namespace OSL.WEB.Controllers;

public class QuestionAnswerController(IQuestionService _questionService, IAnswerService _answerService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> DashboardAsync()
    {
        var question = await _questionService.Get("");

        if (question.IsError)
        {
            return BadRequest(new { error = question.FirstError.Code ?? "An error occurred" });
        }

        return View(question.Value);
    }

    [Authorize]
    [HttpGet("post-question")]
    public IActionResult PostQuestion()
    {
        return View();
    }

    [HttpGet("questions")]
    public async Task<ActionResult<IEnumerable<Question>>> QuestionsList(string searchText = "")
    {
        var question = await _questionService.Get(searchText);

        if (question.IsError)
        {
            return BadRequest(new { error = question.FirstError.Code ?? "An error occurred" });
        }

        return Ok(question.Value);
    }

    [Authorize]
    [HttpPost("question/post-question")]
    public async Task<IActionResult> PostQuestion(QuestionVM model)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Error"] = "Please fill out all the fields properly.";
            return View(model);
        }

        model.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));

        var question = await _questionService.CreateQuestion(model);

        if (question.IsError)
        {
            ViewData["Error"] = question.FirstError.Code ?? "An error occurred";
            return View(model);
        }

        return RedirectToAction("index", "home");
    }

    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> QuestionDetails(int questionId)
    {
        var question = await _questionService.Get(questionId);
        var answer = await _answerService.AnswersWithReplies(questionId);

        if (question.IsError || answer.IsError)
        {
            ViewData["Error"] = question.FirstError.Code ?? answer.FirstError.Code ?? "An error occured.";
            return RedirectToAction("index", "home");
        }

        var model = new QuestionAnswerVM {
            Question = question.Value,
            AnswerVMs = answer.Value
        };

        return View(model);
    }

    [Authorize]
    [HttpPost("answer")]
    public async Task<IActionResult> PostAnswer(AnswerVM model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest("Please fill out all the fields properly.");
        }

        model.UserId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));

        var question = await _answerService.Create(model);

        if (question.IsError)
        {
            return BadRequest(question.FirstError.Description ?? "An error occurred while posting the answer.");
        }

        return RedirectToAction(model.QuestionId.ToString(), "question");
    }

    [Authorize]
    [HttpGet("my-questions")]
    public async Task<IActionResult> MyQuestions()
    {
        var userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));

        var questions = await _questionService.GetMyQuestions(userId);

        if (questions.IsError)
        {
            ViewData["Error"] = questions.FirstError.Code ?? "An error occurred";
            return RedirectToAction("index", "home");
        }

        return View(questions.Value);
    }

    [Authorize]
    [HttpGet("my-responses")]
    public async Task<IActionResult> MyResponses()
    {
        var userId = Convert.ToInt64(HttpContext.Session.GetString("UserId"));

        var answers = await _questionService.GetMyRespondedQuestions(userId);

        if (answers.IsError)
        {
            ViewData["Error"] = answers.FirstError.Code ?? "An error occurred";
            return RedirectToAction("index", "home");
        }

        return View(answers.Value);
    }
}
