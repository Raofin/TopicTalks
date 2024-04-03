using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces.Pdf;

public interface IPdfService
{
    byte[] GenerateQuestionPdf(QuestionWithAnswersDto dto);
}