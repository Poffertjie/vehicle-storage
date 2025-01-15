using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class DeliveryMethod
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CustomerVehicleCheckOut> CustomerVehicleCheckOuts { get; set; } = new List<CustomerVehicleCheckOut>();
}
