using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Core;

public interface IPdfGenerator
{
    Task<byte[]> QuestionPdf(dynamic questionWithAnswersDto);
    Task<byte[]> UserListPdf(List<User> users);
}