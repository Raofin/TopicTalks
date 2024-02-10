using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;
using OSL.BLL.Models;
using OSL.WEB.Attributes;

namespace OSL.WEB.Controllers
{
    public class QuestionController(IQuestionService _questionService) : Controller
    {
        [HttpGet]
        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("QuestionsList")]
        public async Task<IActionResult> QuestionsList()
        {
            var question = await _questionService.Get();
            return View(question.Value);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Index(QuestionVM model)
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
                ViewData["Error"] = question.FirstError.Description ?? "An error occurred";
                return View(model);
            }

            return RedirectToAction("index", "home");
        }
    }
}
