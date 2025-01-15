using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Brand
{
    public int Id { get; set; }

    public string LogoFile { get; set; } = null!;

    public string Name { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public virtual ICollection<BrandModel> BrandModels { get; set; } = new List<BrandModel>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
