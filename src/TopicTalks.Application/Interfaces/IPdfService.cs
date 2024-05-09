using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IPdfService
{
    Task<byte[]> GenerateQuestionPdf(QuestionWithAnswersDto dto);
}