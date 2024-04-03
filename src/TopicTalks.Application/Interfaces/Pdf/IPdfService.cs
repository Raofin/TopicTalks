using DinkToPdf;

namespace TopicTalks.Application.Interfaces.Pdf;

public interface IPdfService
{
    HtmlToPdfDocument CreatePdfObject(string htmlContent);
    byte[] GeneratePdf(string htmlContent);
    byte[] GeneratePdf(HtmlToPdfDocument pdf);
}