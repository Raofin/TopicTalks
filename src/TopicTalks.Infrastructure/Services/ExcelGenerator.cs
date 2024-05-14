using ClosedXML.Excel;
using System.Data;
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

        var dataTable = new DataTable();
        dataTable.Columns.AddRange([
            new DataColumn("User ID", typeof(int)),
            new DataColumn("Username", typeof(string)),
            new DataColumn("Email", typeof(string)),
            new DataColumn("Full Name", typeof(string)),
            new DataColumn("Institute Name", typeof(string)),
            new DataColumn("ID Card Number", typeof(string)),
            new DataColumn("Roles", typeof(string)),
            new DataColumn("Joined At", typeof(string))
        ]);

        foreach (var user in users)
        {
            dataTable.Rows.Add(
                user.UserId,
                user.Username,
                user.Email,
                user.UserDetails?.FullName?? "-",
                user.UserDetails?.InstituteName?? "-",
                user.UserDetails?.IdCardNumber?? "-",
                string.Join(", ", user.UserRoles.Select(ur => (RoleType)ur.RoleId).ToList()),
                user.CreatedAt.ToString("MM-dd-yyyy hh:mm tt")
            );
        }

        worksheet.Cell("A1").InsertTable(dataTable);
        worksheet.Columns().AdjustToContents();
        
        var columnsToCenter = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
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