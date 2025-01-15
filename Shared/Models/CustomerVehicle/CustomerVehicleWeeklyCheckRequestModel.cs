namespace Shared.Models.CustomerVehicle;

public class CustomerVehicleWeeklyCheckRequestModel
{
    public int Id { get; set; }
    public string BatteryChargeStatus { get; set; } = null!;
    public string BatteryStatus { get; set; } = null!;
    public string TirePressureFrontRight { get; set; } = null!;
    public string TirePressureRearRight { get; set; } = null!;
    public string TirePressureRearLeft { get; set; } = null!;
    public string TirePressureFrontLeft { get; set; } = null!;
}