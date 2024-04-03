using DinkToPdf;

namespace TopicTalks.Application.Interfaces.Pdf;

public interface IPdfGeneratorService
{
    HtmlToPdfDocument CreatePdfObject(string htmlContent);
    byte[] GeneratePdf(string htmlContent);
    byte[] GeneratePdf(HtmlToPdfDocument pdf);
}