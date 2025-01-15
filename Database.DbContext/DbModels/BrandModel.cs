using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class BrandModel
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual ICollection<BrandModelVariant> BrandModelVariants { get; set; } = new List<BrandModelVariant>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
