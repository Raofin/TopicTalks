using DinkToPdf;
using DinkToPdf.Contracts;
using TopicTalks.Application.Extensions;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

internal class PdfGenerator(
    IConverter converter,
    IWwwootService wwwootService,
    IUserInfoProvider userInfoProvider) : IPdfGenerator
{
    private readonly IConverter _converter = converter;
    private readonly IWwwootService _wwwoot = wwwootService;
    private readonly IUserInfoProvider _userInfoProvider = userInfoProvider;

    public byte[] GeneratePdf(string htmlContent, bool footerDisable = false, bool showPageNumbers = false)
    {
        var userLocalTime = _userInfoProvider.UserLocalTimeNow();
        var printTime = $"Printed on {userLocalTime.Format3()} at {userLocalTime.Format1()}";

        var pdfDocument = new HtmlToPdfDocument
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
                    WebSettings =
                    {
                        DefaultEncoding = "utf-8",
                        UserStyleSheet = _wwwoot.GetPath("styles", "pdf-styles.css")
                    },
                    HeaderSettings = { Line = false },
                    FooterSettings =
                    {
                        FontName = "Arial",
                        FontSize = 7,
                        Line = false
                    }
                }
            }
        };

        if (footerDisable)
        {
            pdfDocument.Objects[0].FooterSettings = null;
        }
        else if (showPageNumbers)
        {
            pdfDocument.Objects[0].FooterSettings.Center = "Page [page] of [toPage]";
        }
        else if (_userInfoProvider.Username() is not null)
        {
            pdfDocument.Objects[0].FooterSettings.Left = $"Printed by {_userInfoProvider.Username()}";
            pdfDocument.Objects[0].FooterSettings.Center = printTime;
            pdfDocument.Objects[0].FooterSettings.Right = "Page [page] of [toPage]";
        }
        else
        {
            pdfDocument.Objects[0].FooterSettings.Left = printTime;
            pdfDocument.Objects[0].FooterSettings.Right = "Page [page] of [toPage]";
        }

        return _converter.Convert(pdfDocument);
    }
}