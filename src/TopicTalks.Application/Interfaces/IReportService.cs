using ErrorOr;
using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces;

public interface IReportService
{
    Task<ErrorOr<byte[]>> QuestionWithAnswersPdfAsync(long questionId);
    Task<byte[]> UserListPdfAsync();
    Task<ExcelFile> UserListExcelAsync();
}