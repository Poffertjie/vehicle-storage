using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Customer
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public string UserId { get; set; } = null!;

    public string IdentificationNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string IdentificationFile { get; set; } = null!;

    public Guid MODIFIED_BY { get; set; }

    public virtual ICollection<AdditionalContactPerson> AdditionalContactPeople { get; set; } = new List<AdditionalContactPerson>();

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<CustomerStorageContract> CustomerStorageContracts { get; set; } = new List<CustomerStorageContract>();

    public virtual ICollection<CustomerVehicle> CustomerVehicles { get; set; } = new List<CustomerVehicle>();

    public virtual User User { get; set; } = null!;
}
