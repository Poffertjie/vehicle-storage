using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class ServiceAdvisor
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public int CompanyId { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();
}
