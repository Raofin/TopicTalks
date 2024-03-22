using System;
using System.Collections.Generic;

namespace TopicTalks.Domain.Entities;

public partial class UserDetail
{
    public long UserDetailsId { get; set; }

    public long? UserId { get; set; }

    public string? Name { get; set; }

    public string? InstituteName { get; set; }

    public string? IdCardNumber { get; set; }

    public virtual User? User { get; set; }
}
