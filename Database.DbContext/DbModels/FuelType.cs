using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class FuelType
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
