namespace Shared.Models.CustomerVehicle;

public class CustomerVehicleStorageCheckInRequestModel
{
    public int CustomerStorageContractId { get; set; }
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
}