using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Services;

public interface IExcelService
{
    Task<ExcelFile> UserListExcelFile();
}