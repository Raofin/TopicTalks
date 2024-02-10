using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.WEB.Attributes;

namespace OSL.WEB.Controllers
{
    public class QuestionController(IQuestionService _questionService) : Controller
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

            if (question.IsError)
            {
                ViewData["Error"] = question.FirstError.Code ?? "An error occurred";
                return RedirectToAction("index", "home");
            }

            return View(question.Value);
        }
    }
}
