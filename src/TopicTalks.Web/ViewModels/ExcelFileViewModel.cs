namespace TopicTalks.Web.ViewModels;

public record ExcelFile(byte[] Bytes, string ContentType, string Name)
{
    public ExcelFile(byte[] bytes) : this(
        bytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
        $"UsersExport-{DateTime.Now:yyyyMMddHHmmss}.xlsx"
    )
    { }
}
