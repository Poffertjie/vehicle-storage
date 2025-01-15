using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class PaymentOption
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int CompanyId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CustomerStorageContract> CustomerStorageContracts { get; set; } = new List<CustomerStorageContract>();
}
