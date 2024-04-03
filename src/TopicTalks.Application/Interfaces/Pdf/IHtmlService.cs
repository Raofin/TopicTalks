using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces.Pdf;

public interface IHtmlService
{
    string GenerateQuestionPdf(QuestionWithAnswersDto? dto);
}