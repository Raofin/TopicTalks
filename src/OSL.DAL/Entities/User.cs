using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string? Email { get; set; }

    public string? PasswordHash { get; set; }

    public string? Salt { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
