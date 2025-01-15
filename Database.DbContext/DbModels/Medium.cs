using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Medium
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public string File { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;
}
