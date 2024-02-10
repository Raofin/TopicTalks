using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class User
{
    public long UserId { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Question> Questions { get; set; } = new List<Question>();

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
