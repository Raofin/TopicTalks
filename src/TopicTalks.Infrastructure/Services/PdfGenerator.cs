using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

internal class PdfGenerator(IConverter converter, IWebHostEnvironment hostEnvironment) : IPdfGenerator
{
    private readonly IConverter _converter = converter;
    private readonly string _wwwroot = hostEnvironment.ContentRootPath + "/wwwroot";

    public byte[] GeneratePdf(string htmlContent)
    {
        var pdf = CreatePdfObject(htmlContent);
        return _converter.Convert(pdf);
    }

    private HtmlToPdfDocument CreatePdfObject(string htmlContent)
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
                        Center = "Printed on " + DateTime.UtcNow.ToString("MMMM dd, yyyy hh:mm tt")
                    }
                }
            }
        };
    }
}