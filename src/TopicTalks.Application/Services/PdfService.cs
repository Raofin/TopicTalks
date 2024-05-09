using System.Text;
using Microsoft.Extensions.Hosting;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Extensions;
using TopicTalks.Application.Interfaces;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Application.Services;

internal class PdfService(IPdfGenerator pdfGenerator, IWwwootService wwwoot, IUserInfoProvider userInfoProviderProvider) : IPdfService
{
    private readonly IPdfGenerator _pdfGenerator = pdfGenerator;
    private readonly IWwwootService _wwwoot = wwwoot;
    private readonly IUserInfoProvider _userInfoProvider = userInfoProviderProvider;

    public byte[] GenerateQuestionPdf(QuestionWithAnswersDto dto)
    {
        var logoUri = _wwwoot.GetDataUri("logo.svg");
        
        var sb = new StringBuilder();

        sb.Append(@"
<head>
  <style>
    .header {
      text-align: center;
    }

    .header img {
      height: 80px;
    }

    .container {
      background-color: #f4f4f4;
      border-radius: 10px;
      overflow: hidden;
      margin-bottom: 20px;
      page-break-inside: avoid;
    }

    .question {
      padding: 20px;
    }

    .answer {
      padding: 17px 17px;
      margin-bottom: 20px;
    }

    .content-header {
      margin-bottom: 10px;
      display: -webkit-box;
      -webkit-box-pack: justify;
    }

    .author {
      display: -webkit-box;
      -webkit-box-align: end;
    }

    .image {
      width: 23px;
      height: 23px;
      margin-right: 5px;
      border-radius: 50%;
      overflow: hidden;
      display: inline-block;
    }

    .image img {
      width: 100%;
      height: auto;
      display: block;
    }

    .cover {
      margin-bottom: -5px;
      text-align: center;
      max-height: 200px;
      display: -webkit-box;
      -webkit-box-align: center;
      overflow: hidden;
    }

    .cover img {
      max-width: 100%;
      margin-top: -15%;
    }
  </style>
</head>
        ");

        sb.Append($"""
                   <body>
                       <div class='header'>
                           <img src='{logoUri}' />
                       </div>
                       <div class='container'>
                       
                   """);
        if (dto.ImageFile is not null)
        {
            sb.Append($"""
                          <div class='cover'>
                              <img src='{dto.ImageFile.DirectLink}' />
                          </div>
                       """);
        }
        
        sb.Append($"""
                           <div class='question avoid-page-break'>
                               <div class='content-header'>
                                   <div class='author'>
                                       <div class='image'>
                                           <img src='{dto.UserInfo?.ProfileImageUrl ?? _wwwoot.GetDataUri("user.svg")}' />
                                       </div>
                                       <div><strong>{dto.UserInfo?.Username ?? "Deleted User"}</strong></div>
                                   </div>
                                   <div>{_userInfoProvider.UtcToUserLocalTime(dto.CreatedAt).Format4()} ({dto.CreatedAt.ToTimeAgo()})</div>
                               </div>
                               <div style='margin-bottom: 10px'>
                                   <strong>Question: </strong> {dto.Explanation}
                               </div>
                               <div><strong>Topics:</strong> {dto.Topic}</div>
                           </div>
                       </div>
                   """);

        if (dto.Answers is not null)
        {
            sb.Append(RenderAnswers(new StringBuilder(), dto.Answers));
        }

        sb.Append("""
                       </div>
                   </body>
                   """);

        return _pdfGenerator.GeneratePdf(sb.ToString());
    }

    private StringBuilder RenderAnswers(StringBuilder sb, List<AnswerWithRepliesDto> answers, int marginLeft = 40)
    {
        foreach (var answer in answers)
        {
            sb.Append($"""
                      <div class='container answer' style='margin-left: {marginLeft}px'>
                          <div class='content-header'>
                              <div class='author'>
                                  <div class='image'>
                                      <img src='{answer.UserInfo?.ProfileImageUrl ?? _wwwoot.GetDataUri("user.svg")}'/>
                                  </div>
                                  <div><strong>{answer.UserInfo?.Username ?? "Deleted User"}</strong></div>
                              </div>
                              <div>{_userInfoProvider.UtcToUserLocalTime(answer.CreatedAt).Format4()} ({answer.CreatedAt.ToTimeAgo()})</div>
                          </div>
                          <div>{answer.Explanation}</div>
                      </div>
                      """);

            if (answer.Answers is not null)
            {
                RenderAnswers(sb, answer.Answers, marginLeft + 20);
            }
        }
        
        return sb;
    }
}