using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class Company
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string ContactNumber { get; set; } = null!;

    public string? LogoFile { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public virtual ICollection<CustomerStorageContract> CustomerStorageContracts { get; set; } = new List<CustomerStorageContract>();

    public virtual ICollection<CustomerVehicleCheckIn> CustomerVehicleCheckIns { get; set; } = new List<CustomerVehicleCheckIn>();

    public virtual ICollection<CustomerVehicleCheckOut> CustomerVehicleCheckOuts { get; set; } = new List<CustomerVehicleCheckOut>();

    public virtual ICollection<CustomerVehicleDailyCheck> CustomerVehicleDailyChecks { get; set; } = new List<CustomerVehicleDailyCheck>();

    public virtual ICollection<CustomerVehicleStatus> CustomerVehicleStatuses { get; set; } = new List<CustomerVehicleStatus>();

    public virtual ICollection<CustomerVehicleStorageCheckIn> CustomerVehicleStorageCheckIns { get; set; } = new List<CustomerVehicleStorageCheckIn>();

    public virtual ICollection<CustomerVehicleWeeklyCheck> CustomerVehicleWeeklyChecks { get; set; } = new List<CustomerVehicleWeeklyCheck>();

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Medium> Media { get; set; } = new List<Medium>();

    public virtual ICollection<PaymentOption> PaymentOptions { get; set; } = new List<PaymentOption>();

    public virtual ICollection<StorageBayStatus> StorageBayStatuses { get; set; } = new List<StorageBayStatus>();

    public virtual ICollection<StorageBay> StorageBays { get; set; } = new List<StorageBay>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
