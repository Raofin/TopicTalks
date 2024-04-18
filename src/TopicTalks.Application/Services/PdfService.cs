using System.Text;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class PdfService(IPdfGenerator pdfGeneratorService) : IPdfService
{
    private readonly IPdfGenerator _pdfGeneratorService = pdfGeneratorService;

    public byte[] GenerateQuestionPdf(QuestionWithAnswersDto dto)
    {
        var sb = new StringBuilder();

        sb.Append(@"
            <head>
                <style>
                    .question-container {
                        margin: 60px 18px;
                        background-color: #f4f4f4;
                        border-radius: 10px;
                        box-shadow: 0px 0px 3px 0px rgba(0,0,0,0.1);
                        padding: 20px;
                    }
                    .question-header {
                        font-family: 'TiltWarp', sans-serif;
                        background-color: #007bff;
                        weight: normal;
                        font-size: 40px;
                        color: #fff;
                        text-align: center;
                        padding: 2px;
                        border-top-left-radius: 10px;
                        border-top-right-radius: 10px;
                    }
                    .question-content {
                        font-size: 18px;
                    }
                    .question-info {
                        font-size: 14px;
                        color: #666;
                    }
                    .question-info span {
                        font-weight: bold;
                        color: #333;
                    }
                </style>
            </head>
        ");

        sb.Append($@"
            <body>
                <div class='question-container'>
                    <div class='question-header'>
                        <p>TopicTalks</p>
                    </div>
                    <div class='question-content'>
                        <p>
                            <strong>Author:</strong> {dto.UserInfo?.Email ?? "Deleted User"}
                            <span style='float:right'><strong>Date:</strong> {dto.CreatedAt.ToString("MMM dd, yyyy | hh:mm tt")}</span>
                        </p>
                        <p><strong>Question:</strong> {dto.Explanation}</p>
                        <p><strong>Topics:</strong> {dto.Topic}</p>
                    </div>
                </div>
            </body>
        ");

        return _pdfGeneratorService.GeneratePdf(sb.ToString());
    }
}