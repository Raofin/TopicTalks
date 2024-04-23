using ClosedXML.Excel;
using TopicTalks.Domain.Entities;
using TopicTalks.Domain.Enums;
using TopicTalks.Domain.Interfaces.Core;

namespace TopicTalks.Infrastructure.Services;

internal class ExcelGenerator : IExcelGenerator
{
    public byte[] UserListExcel(List<User> users)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Users");

        var usersWithCustomData = users.Select(user => new
        {
            user.UserId,
            user.Email,
            CreatedAt = user.CreatedAt.ToString("MM-dd-yyyy hh:mm tt"),
            Name = user.UserDetails?.FullName ?? "-",
            InstituteName = user.UserDetails?.InstituteName ?? "-",
            IdCardNumber = user.UserDetails?.IdCardNumber ?? "-",
            Roles = string.Join(", ", user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList())
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

        return stream.ToArray();
    }
}