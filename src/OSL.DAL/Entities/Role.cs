﻿using System;
using System.Collections.Generic;

namespace OSL.DAL.Entities;

public partial class Role
{
    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;
}