using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class BrandModelVariant
{
    public int Id { get; set; }

    public int BrandModelId { get; set; }

    public string Name { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public virtual BrandModel BrandModel { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
