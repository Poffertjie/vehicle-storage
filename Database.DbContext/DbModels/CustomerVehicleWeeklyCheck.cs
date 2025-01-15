using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerVehicleWeeklyCheck
{
    public int Id { get; set; }

    public int CustomerVehicleId { get; set; }

    public string? BatteryChargeStatus { get; set; }

    public string? BatteryStatus { get; set; }

    public string? TirePressureFrontRight { get; set; }

    public string? TirePressureRearRight { get; set; }

    public string? TirePressureRearLeft { get; set; }

    public string? TirePressureFrontLeft { get; set; }

    public bool Completed { get; set; }

    public DateTime Date { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public int CustomerStorageContractId { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual CustomerStorageContract CustomerStorageContract { get; set; } = null!;

    public virtual CustomerVehicle CustomerVehicle { get; set; } = null!;
}
