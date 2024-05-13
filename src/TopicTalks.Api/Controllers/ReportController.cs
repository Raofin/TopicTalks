using ErrorOr;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopicTalks.Application.Attributes;
using TopicTalks.Application.Interfaces;

namespace TopicTalks.Api.Controllers;

public class ReportController(IReportService reportService) : ApiController
{
    private readonly IReportService _reportService = reportService;
    
    [AllowAnonymous]
    [HttpGet("pdf/question/{questionId}")]
    public async Task<IActionResult> GetPdf(long questionId)
    {
        var pdf = await _reportService.QuestionWithAnswersPdfAsync(questionId);

        return !pdf.IsError
            ? File(pdf.Value, "application/pdf")
            : pdf.FirstError.Type switch {
                ErrorType.NotFound => NotFound("Question was not found."),
                _ => Problem("An unexpected error occurred.")
            };
    }
    
    [AuthorizeModerator]
    [HttpGet("pdf/users")]
    public async Task<IActionResult> GetPdf()
    {
        var pdf = await _reportService.UserListPdfAsync();

        return File(pdf, "application/pdf");
    }

    [AuthorizeModerator]
    [HttpGet("excel/users")]
    public async Task<IActionResult> GetExcel()
    {
        var excelFile = await _reportService.UserListExcelAsync();

        return File(excelFile.Bytes, excelFile.ContentType, excelFile.Name);
    }
}