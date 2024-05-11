using TopicTalks.Application.Dtos;
using TopicTalks.Domain.Entities;

namespace TopicTalks.Application.Interfaces;

public interface IPdfService
{
    Task<byte[]> GenerateQuestionPdf(QuestionWithAnswersDto dto);
    Task<byte[]> GenerateUserListPdf(List<User> users);
}