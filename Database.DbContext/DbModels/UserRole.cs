﻿using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class UserRole
{
    public int Id { get; set; }

    public string RoleId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
