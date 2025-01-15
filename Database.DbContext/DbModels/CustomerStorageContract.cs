using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerStorageContract
{
    public int Id { get; set; }

    public int CompanyId { get; set; }

    public int CustomerId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public decimal PricePerMonth { get; set; }

    public int PaymentOptionId { get; set; }

    public int StorageBayId { get; set; }

    public bool IsChargerSupplied { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CustomerVehicleId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual CustomerVehicle CustomerVehicle { get; set; } = null!;

    public virtual ICollection<CustomerVehicleCheckIn> CustomerVehicleCheckIns { get; set; } = new List<CustomerVehicleCheckIn>();

    public virtual ICollection<CustomerVehicleCheckOut> CustomerVehicleCheckOuts { get; set; } = new List<CustomerVehicleCheckOut>();

    public virtual ICollection<CustomerVehicleDailyCheck> CustomerVehicleDailyChecks { get; set; } = new List<CustomerVehicleDailyCheck>();

    public virtual ICollection<CustomerVehicleStorageCheckIn> CustomerVehicleStorageCheckIns { get; set; } = new List<CustomerVehicleStorageCheckIn>();

    public virtual ICollection<CustomerVehicleWeeklyCheck> CustomerVehicleWeeklyChecks { get; set; } = new List<CustomerVehicleWeeklyCheck>();

    public virtual PaymentOption PaymentOption { get; set; } = null!;

    public virtual StorageBay StorageBay { get; set; } = null!;
}
