using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerVehicle
{
    public int Id { get; set; }

    public int StatusId { get; set; }

    public int VehicleId { get; set; }

    public string LicensePlate { get; set; } = null!;

    public string ChassisVin { get; set; } = null!;

    public DateTime RegistrationExpiryDate { get; set; }

    public decimal AverageVehicleValuation { get; set; }

    public DateTime NextService { get; set; }

    public int ServiceAdvisorId { get; set; }

    public string VehicleRegistrationFile { get; set; } = null!;

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public int CustomerId { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual ICollection<CustomerStorageContract> CustomerStorageContracts { get; set; } = new List<CustomerStorageContract>();

    public virtual ICollection<CustomerVehicleCheckIn> CustomerVehicleCheckIns { get; set; } = new List<CustomerVehicleCheckIn>();

    public virtual ICollection<CustomerVehicleCheckOut> CustomerVehicleCheckOuts { get; set; } = new List<CustomerVehicleCheckOut>();

    public virtual ICollection<CustomerVehicleDailyCheck> CustomerVehicleDailyChecks { get; set; } = new List<CustomerVehicleDailyCheck>();

    public virtual ICollection<CustomerVehicleStorageCheckIn> CustomerVehicleStorageCheckIns { get; set; } = new List<CustomerVehicleStorageCheckIn>();

    public virtual ICollection<CustomerVehicleWeeklyCheck> CustomerVehicleWeeklyChecks { get; set; } = new List<CustomerVehicleWeeklyCheck>();

    public virtual ServiceAdvisor ServiceAdvisor { get; set; } = null!;

    public virtual CustomerVehicleStatus Status { get; set; } = null!;

    public virtual Vehicle Vehicle { get; set; } = null!;
}
