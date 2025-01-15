namespace Shared.Models.CustomerVehicle;

public class GetVehicleChecksResponseModel
{
    public int CustomerStorageContractId { get; set; }
    public int StorageBay { get; set; }
    public int CustomerVehicleId { get; set; }
    public string Customer  { get; set; }
    public string Brand  { get; set; }
    public string Model { get; set; }
    public string LicensePlate { get; set; }
    public string ChassisVin { get; set; }
}