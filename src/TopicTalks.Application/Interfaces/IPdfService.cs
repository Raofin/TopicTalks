using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IPdfService
{
    byte[] GenerateQuestionPdf(QuestionWithAnswersDto dto);
}