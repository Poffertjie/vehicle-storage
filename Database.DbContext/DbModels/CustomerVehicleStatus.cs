using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerVehicleStatus
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();
}
