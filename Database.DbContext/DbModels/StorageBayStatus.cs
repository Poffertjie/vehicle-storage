using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class StorageBayStatus
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<StorageBay> StorageBays { get; set; } = new List<StorageBay>();
}
