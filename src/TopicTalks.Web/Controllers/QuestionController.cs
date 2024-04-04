using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Web.Extensions;
using TopicTalks.Web.Services;
using TopicTalks.Web.ViewModels;

namespace TopicTalks.Web.Controllers
{
    [Authorize]
    [Route("question")]
    public class QuestionController(IHttpService httpService) : Controller
    {
        private readonly IHttpService _httpService = httpService;

        [HttpPatch]
        public async Task<IActionResult> UpdateQuestion(QuestionUpdateViewModel request)
        {
            var response = await _httpService.Client.PatchAsync("api/question", request.ToStringContent());

            return response.IsSuccessStatusCode
                ? Ok(response.DeserializeTo<QuestionWithAnswersViewModel>())
                : new StatusCodeResult((int)response.StatusCode);
        }

        [HttpDelete("{questionId}")]
        public async Task<IActionResult> Delete(long questionId)
        {
            var response = await _httpService.Client.DeleteAsync($"api/question/{questionId}");

            return response.IsSuccessStatusCode 
                ? Ok() 
                : new StatusCodeResult((int)response.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion(QuestionCreateViewModel request)
        {
            var response = await _httpService.Client.PostAsync("api/question", request.ToStringContent());

            return response.IsSuccessStatusCode
                ? Ok(response.DeserializeTo<QuestionWithAnswersViewModel>())
                : new StatusCodeResult((int)response.StatusCode);
        }

        [HttpGet("pdf/{questionId}")]
        public async Task<IActionResult> GetPdf(long questionId)
        {
            var response = await _httpService.Client.GetAsync($"api/question/pdf/{questionId}");

            return response.IsSuccessStatusCode
                ? File(await response.Content.ReadAsByteArrayAsync(), "application/pdf")
                : new StatusCodeResult((int)response.StatusCode);
        }
    }
}
