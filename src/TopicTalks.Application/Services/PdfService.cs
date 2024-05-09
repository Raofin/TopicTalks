using Razor.Templating.Core;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class PdfService(IPdfGenerator pdfGenerator) : IPdfService
{
    private readonly IPdfGenerator _pdfGenerator = pdfGenerator;

    public async Task<byte[]> GenerateQuestionPdf(QuestionWithAnswersDto dto)
    {
        var html = await RazorTemplateEngine.RenderAsync("Templates/Discussion.cshtml", dto);

        return _pdfGenerator.GeneratePdf(html);
    }
}