namespace TopicTalks.Application.Dtos;

public record ExcelFile(byte[] Bytes, string ContentType, string Name)
{
    public ExcelFile(byte[] bytes) : this(
        Bytes: bytes, 
        ContentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", 
        Name: $"UsersExport-{DateTime.UtcNow:yyyyMMddHHmmss}.xlsx"
    ) { }
}
