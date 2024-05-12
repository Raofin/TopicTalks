using DinkToPdf.Contracts;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services.Pdf;

public class PdfGenerator(
    IWwwootService wwwootService, 
    IUserInfoProvider userInfoProvider,
    IRazorEngine razorEngine,
    IConverter converter) : PdfDocument(wwwootService, userInfoProvider, converter), IPdfGenerator
{
    private readonly IRazorEngine _razorEngine = razorEngine;

    public async Task<byte[]> QuestionPdf(dynamic questionWithAnswersDto)
    {
        var html = await _razorEngine.RenderAsync("Templates/Discussion.cshtml", questionWithAnswersDto);
        var pdfDocument = CreatePdfDocument(html);

        return GeneratePdf(pdfDocument);
    }
    
    public async Task<byte[]> UserListPdf(List<User> users)
    {
        var html = await _razorEngine.RenderAsync("Templates/UserList.cshtml", users);
        var pdfDocument = CreatePdfDocument(html);

        pdfDocument.Objects[0].FooterSettings.Left = null;
        pdfDocument.Objects[0].FooterSettings.Right = null;
        pdfDocument.Objects[0].FooterSettings.Center = PrintTime;

        return GeneratePdf(pdfDocument);
    }
}