using DinkToPdf;
using DinkToPdf.Contracts;
using TopicTalks.Application.Extensions;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services.Pdf;

public class PdfDocument(IWwwootService wwwootService, IUserInfoProvider userInfoProvider, IConverter converter)
{
    private readonly IWwwootService _wwwoot = wwwootService;
    private readonly IUserInfoProvider _userInfoProvider = userInfoProvider;
    private readonly IConverter _converter = converter;

    protected string PrintBy => $"Printed by {_userInfoProvider.Username()}";

    protected string PageNumber => "Page [page] of [toPage]";

    protected string PrintTime
    {
        get
        {
            var userLocalTime = _userInfoProvider.UserLocalTimeNow();
            return $"Printed on {userLocalTime.Format3()} at {userLocalTime.Format1()}";
        }
    }

    protected HtmlToPdfDocument CreatePdfDocument(string htmlContent, bool footerDisable = false)
    {
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
        else if (_userInfoProvider.Username() is not null)
        {
            pdfDocument.Objects[0].FooterSettings.Left = PrintBy;
            pdfDocument.Objects[0].FooterSettings.Center = PrintTime;
            pdfDocument.Objects[0].FooterSettings.Right = PageNumber;
        }
        else
        {
            pdfDocument.Objects[0].FooterSettings.Left = PrintTime;
            pdfDocument.Objects[0].FooterSettings.Right = PageNumber;
        }

        return pdfDocument;
    }
    
    protected byte[] GeneratePdf(HtmlToPdfDocument pdfDocument)
    {
        return _converter.Convert(pdfDocument);
    }
}