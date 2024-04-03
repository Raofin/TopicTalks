using System.Text;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces.Pdf;

namespace TopicTalks.Application.Services.Pdf;

internal class HtmlService : IHtmlService
{
    public string GenerateQuestionPdf(QuestionWithAnswersDto? dto)
    {
        var sb = new StringBuilder();

        sb.Append(@"
            <head>
                <style>
                    .container {
                        margin: 60px 18px;
                        background-color: #f4f4f4;
                        border-radius: 10px;
                        box-shadow: 0px 0px 3px 0px rgba(0,0,0,0.1);
                        padding: 20px;
                    }
                    .header {
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
            <body>
                <div class='container'>
                    <div class='header'>
                        <p>TopicTalks</p>
                    </div>
                    <div class='question-content'>
                        <p><strong>Author:</strong> hello@rawfin.net<span style='float:right'><strong>Date:</strong> Mar 22, 2024 | 07:42 PM (11 days ago)</span></p>
                        <p><strong>Question:</strong>
                        In C# 12, what are the advantages and trade-offs of using record types with pattern matching and deconstruction in ASP.NET 8 code, considering maintainability, readability, and potential performance implications?</p>
                        <p><strong>Topics:</strong> C# 12, Code Syntax, Maintainability Maintainability</p>
                    </div>
                </div>
            </body>
        ");

        return sb.ToString();
    }
}