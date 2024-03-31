namespace TopicTalks.Domain.Entities;

public class Role
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}