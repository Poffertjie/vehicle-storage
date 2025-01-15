using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class StorageBay
{
    public int Id { get; set; }

    public int Number { get; set; }

    public int StorageBayStatusId { get; set; }

    public int CompanyId { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CustomerStorageContract> CustomerStorageContracts { get; set; } = new List<CustomerStorageContract>();

    public virtual StorageBayStatus StorageBayStatus { get; set; } = null!;
}
