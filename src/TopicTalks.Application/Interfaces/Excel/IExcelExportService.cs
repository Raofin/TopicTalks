using TopicTalks.Application.Dtos;

namespace TopicTalks.Application.Interfaces.Excel;

internal interface IExcelExportService
{
    ExcelFile UserListExcel(IEnumerable<UserDto> users);
}