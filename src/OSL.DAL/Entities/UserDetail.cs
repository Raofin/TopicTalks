using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class UserDetail
{
    public long UserDetailsId { get; set; }

    public long? UserId { get; set; }

    public string Name { get; set; } = null!;

    public string InstituteName { get; set; } = null!;

    public string IdCardNumber { get; set; } = null!;

    public virtual User? User { get; set; }
}
