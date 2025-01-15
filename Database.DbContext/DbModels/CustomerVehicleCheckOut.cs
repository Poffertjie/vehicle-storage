using System;
using System.Collections.Generic;

namespace Database.DbContext.DbModels;

public partial class CustomerVehicleCheckOut
{
    public int Id { get; set; }

    public int CustomerVehicleId { get; set; }

    public bool ReturnAllBelongings { get; set; }

    public bool ReturnNumberPlates { get; set; }

    public DateTime CheckOutDate { get; set; }

    public DateTime PlannedCheckInDate { get; set; }

    public DateTime? CheckInDate { get; set; }

    public int DeliveryMethodId { get; set; }

    public string? Address { get; set; }

    public Guid MODIFIED_BY { get; set; }

    public int CompanyId { get; set; }

    public int CustomerStorageContractId { get; set; }

    public string? ErrorCodeImage { get; set; }

    public DateTime TimeStamp { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual CustomerStorageContract CustomerStorageContract { get; set; } = null!;

    public virtual CustomerVehicle CustomerVehicle { get; set; } = null!;

    public virtual DeliveryMethod DeliveryMethod { get; set; } = null!;
}
