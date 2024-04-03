using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using TopicTalks.Application.Interfaces.Pdf;

namespace TopicTalks.Application.Services.Pdf;

internal class PdfService(IConverter converter, IWebHostEnvironment hostEnvironment) : IPdfService
{
    private readonly IConverter _converter = converter;
    private readonly string _wwwroot = hostEnvironment.ContentRootPath + "/wwwroot";

    public HtmlToPdfDocument CreatePdfObject(string htmlContent)
    {
        return new HtmlToPdfDocument
        {
            GlobalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                DocumentTitle = "TopicTalks",
                DPI = 300,
            },
            Objects =
            {
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = $"{_wwwroot}/pdf-styles.css" },
                    HeaderSettings = { Line = false },
                    FooterSettings =
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Line = false,
                        Right = "Page [page] of [toPage]",
                        Center = "Printed on " + DateTime.Now.ToString("MMMM dd, yyyy hh:mm tt")
                    }
                }
            }
        };
    }

    public byte[] GeneratePdf(string htmlContent)
    {
        var pdf = CreatePdfObject(htmlContent);
        return _converter.Convert(pdf);
    }

    public byte[] GeneratePdf(HtmlToPdfDocument pdf)
    {
        return _converter.Convert(pdf);
    }
}