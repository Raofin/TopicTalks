using System;
using System.Collections.Generic;

namespace TopicTalks.Domain.Entities;

public partial class Role
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
