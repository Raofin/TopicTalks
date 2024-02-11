using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.WEB.Attributes;

namespace OSL.WEB.Controllers
{
    public class QuestionController(IQuestionService _questionService, IAnswerService _answerService) : Controller
    {
        [Authorize]
        [HttpGet("post-question")]
        public IActionResult PostQuestion()
        {
            return View();
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard()
        {
            var question = await _questionService.Get();

            if (question.IsError)
            {
                ViewData["Error"] = question.FirstError.Code ?? "An error occurred";
                return RedirectToAction("index", "home");
            }

            return View(question.Value);
        }

        [Authorize]
        [HttpPost("post-question")]
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

            return RedirectToAction("dashboard", "questions");
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

        /*[HttpGet("answer/{questionId}")]
        public async Task<IActionResult> QuestionDetails(long questionId)
        {
            var answer = await _answerService.AnswersWithReplies(questionId);

            if (answer.IsError)
            {
                ViewData["Error"] = answer.FirstError.Code ?? "An error occurred";
                return RedirectToAction("index", "home");
            }

            return View(answer.Value);
        }*/
    }
}
