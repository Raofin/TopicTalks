using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Interfaces.Pdf;

namespace TopicTalks.Api.Controllers;

public class TestController(IPdfService pdfService, IHtmlService htmlService) : Controller
{
    private readonly IPdfService _pdfService = pdfService;
    private readonly IHtmlService _htmlService = htmlService;


    [HttpGet("pdf")]
    public IActionResult GetPdf()
    {
        var html = _htmlService.GenerateQuestionPdf(null);
        byte[] pdf = _pdfService.GeneratePdf(html);

        return File(pdf, "application/pdf");
    }
}