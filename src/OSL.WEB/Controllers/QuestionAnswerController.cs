using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Enums;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.BLL.Services;
using OSL.WEB.Extensions;

namespace OSL.WEB.Controllers;

public class QuestionAnswerController(
        IQuestionService _questionService, 
        IAnswerService _answerService, 
        IAuthService _authService
    ) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Dashboard()
    {
        var question = await _questionService.Get("");

        if (question.IsError)
        {
            return BadRequest(question.ErrorDescription());
        }

        return View(question.Value);
    }

    [HttpGet("questions")]
    public async Task<ActionResult> QuestionsList(string searchText = "")
    {
        var question = await _questionService.Get(searchText);

        if (question.IsError)
        {
            return BadRequest(question.ErrorDescription());
        }

        return Ok(question.Value);
    }

    [HttpGet("post-question")]
    public IActionResult PostQuestion()
    {
        return View();
    }

    [HttpPost("question/post-question")]
    public async Task<IActionResult> PostQuestion(QuestionVM model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.ValidationFailed();
        }

        model.UserId = long.Parse(_authService.UserId);

        var question = await _questionService.CreateQuestion(model);

        if (question.IsError)
        {
            return BadRequest(question.ErrorDescription());
        }

        return Ok(question.Value);
    }

    [HttpGet("question/{questionId}")]
    public async Task<IActionResult> QuestionDetails(int questionId)
    {
        var question = await _questionService.Get(questionId);
        var answer = await _answerService.AnswersWithReplies(questionId);
        var hasTeachersAnswer = await _answerService.HasTeachersAnswer(questionId);

        if (question.IsError)
        {
            ViewData["Error"] = question.ErrorDescription();
            return RedirectToAction("index", "default");
        }

        var model = new QuestionAnswerVM {
            Question = question.Value,
            AnswerVMs = answer.Value.OrderByDescending(answer => answer.CreatedAt).ToList(),
            HasTeachersAnswer = hasTeachersAnswer.Value
        };


        return View(model);
    }

    [HttpPost("answer")]
    public async Task<IActionResult> PostAnswer(AnswerVM model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.ValidationFailed();
        }

        model.UserId = long.Parse(_authService.UserId);

        var answer = await _answerService.Create(model);

        if (answer.IsError)
        {
            return BadRequest(answer.ErrorDescription());
        }

        var ans = new AnswerVM {
            AnswerId = answer.Value.AnswerId,
            ParentAnswerId = answer.Value.ParentAnswerId,
            QuestionId = answer.Value.QuestionId,
            Explanation = answer.Value.Explanation,
            UserId = answer.Value.UserId,
            Email = answer.Value.User?.Email,
            CreatedAt = answer.Value.CreatedAt
        };

        return Ok(ans);
    }

    [HttpGet("my-questions")]
    public async Task<IActionResult> MyQuestions()
    {
        var userId = long.Parse(_authService.UserId);

        var questions = await _questionService.GetMyQuestions(userId);

        if (questions.IsError)
        {
            ViewData["Error"] = questions.ErrorDescription();
            return RedirectToAction("index", "default");
        }

        return View(questions.Value);
    }

    [HttpGet("my-responses")]
    public async Task<IActionResult> MyResponses()
    {
        var userId = long.Parse(_authService.UserId);

        var questions = await _questionService.GetMyRespondedQuestions(userId);

        if (questions.IsError)
        {
            ViewData["Error"] = questions.ErrorDescription();
            return RedirectToAction("index", "default");
        }

        return View(questions.Value);
    }

    [HttpDelete("delete-question")]
    public async Task<IActionResult> DeleteQuestion(long questionId)
    {
        var question = await _questionService.Get(questionId);

        if (question.IsError)
        {
            return BadRequest(question.ErrorDescription());
        }

        else if (_authService.UserRole == RoleType.Moderator.ToString() || long.Parse(_authService.UserId) == question.Value.UserId)
        {
            var deleteQuestion = await _questionService.DeleteQuestion(questionId);

            if (deleteQuestion.IsError)
            {
                return BadRequest(deleteQuestion.ErrorDescription());
            }

            return Ok(questionId);
        }

        return Unauthorized("You dont have permission to delete that question.");
    }

    [HttpDelete("delete-answer")]
    public async Task<IActionResult> DeleteAnswer(long answerId)
    {
        var answer = await _answerService.Get(answerId);

        if (answer.IsError)
        {
            return BadRequest(answer.ErrorDescription());
        }

        else if (_authService.UserRole == RoleType.Moderator.ToString() || long.Parse(_authService.UserId) == answer.Value.UserId)
        {
            var deleteAnswer = await _answerService.Delete(answerId);

            if (deleteAnswer.IsError)
            {
                return BadRequest(deleteAnswer.ErrorDescription());
            }

            return Ok(answerId);
        }

        return Unauthorized("You dont have permission to delete that question.");
    }
}
