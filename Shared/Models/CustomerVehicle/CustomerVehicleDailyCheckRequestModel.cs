namespace Shared.Models.CustomerVehicle;

public class CustomerVehicleDailyCheckRequestModel
{
    public int Id { get; set; }
    public string BatteryChargeStatus { get; set; } = null!;
    public string TyreCheck { get; set; } = null!;
    public string OilCheck { get; set; } = null!;
    public string WaterCheck { get; set; } = null!;
    public string SpeedometerImage { get; set; } = null!;
    public int CustomerStorageContractId { get; set; }
    public int StorageBayId { get; set; }
}