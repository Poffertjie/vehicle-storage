using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Vehicle
{
    public int Id { get; set; }

    public int BrandId { get; set; }

    public int BrandModelId { get; set; }

    public int? BrandModelVariantId { get; set; }

    public int FuelTypeId { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public string Description { get; set; } = null!;

    public int Year { get; set; }

    public virtual Brand Brand { get; set; } = null!;

    public virtual BrandModel BrandModel { get; set; } = null!;

    public virtual BrandModelVariant? BrandModelVariant { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();

    public virtual FuelType FuelType { get; set; } = null!;
}
