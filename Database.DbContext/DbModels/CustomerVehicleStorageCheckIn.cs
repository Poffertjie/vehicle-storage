using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerVehicleStorageCheckIn
{
    public int Id { get; set; }

    public int CustomerVehicleId { get; set; }

    public string VehicleOnChargeImage { get; set; } = null!;

    public string KilometerReadingImage { get; set; } = null!;

    public string VehicleUnderCoverImage { get; set; } = null!;

    public bool ParkedTheVehicleOnTireCradles { get; set; }

    public bool ChargerConnected { get; set; }

    public bool SeatCover { get; set; }

    public bool SteeringCover { get; set; }

    public bool FloorMats { get; set; }

    public bool KeyTagged { get; set; }

    public bool KeyPlacedInSafe { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public int CustomerStorageContractId { get; set; }

    public DateTime TimeStamp { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual CustomerStorageContract CustomerStorageContract { get; set; } = null!;

    public virtual CustomerVehicle CustomerVehicle { get; set; } = null!;
}
