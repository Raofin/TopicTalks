using Microsoft.AspNetCore.Mvc;

namespace OSL.WEB.Controllers
{
    public class QuestionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
