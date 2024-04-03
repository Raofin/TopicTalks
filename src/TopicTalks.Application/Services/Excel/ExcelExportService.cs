using ClosedXML.Excel;
using TopicTalks.Application.Dtos;
using TopicTalks.Application.Interfaces.Excel;

namespace TopicTalks.Application.Services.Excel;

internal class ExcelExportService : IExcelExportService
{
    public ExcelFile UserListExcel(IEnumerable<UserDto> users)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Users");

        var usersWithCustomData = users.Select(user => new {
            UserId = user.UserId,
            Email = user.Email,
            CreatedAt = user.CreatedAt.ToString("MM-dd-yyyy hh:mm tt"),
            Name = user.UserDetails?.Name ?? "-",
            InstituteName = user.UserDetails?.InstituteName ?? "-",
            IdCardNumber = user.UserDetails?.IdCardNumber ?? "-",
            Roles = string.Join(", ", user.Roles)
        });

        worksheet.Cell("A1").InsertTable(usersWithCustomData);
        worksheet.Columns().AdjustToContents();

        var columnsToCenter = new[] { 1, 3, 4, 5, 6 };
        foreach (var columnIndex in columnsToCenter)
        {
            worksheet.Column(columnIndex).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return new ExcelFile(stream.ToArray());
    }
}