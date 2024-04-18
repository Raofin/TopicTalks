using TopicTalks.Domain.Entities;

namespace TopicTalks.Domain.Interfaces.Core;

public interface IExcelGenerator
{
    byte[] UserListExcel(List<User> users);
}