using Microsoft.AspNetCore.Mvc;
using OSL.BLL.Interfaces;

namespace OSL.WEB.Controllers;

public class AnswerController(IAnswerService _answerService) : Controller
{
    [HttpGet("answer/{questionId}")]
    public async Task<IActionResult> IndexAsync(long questionId)
    {
        var ans = await _answerService.AnswersWithReplies(questionId);


        return Ok(ans);
    }
}
