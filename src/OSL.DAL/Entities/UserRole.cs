using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class UserRole
{
    public long UserRoleId { get; set; }

    public long UserId { get; set; }

    public long RoleId { get; set; }
}
