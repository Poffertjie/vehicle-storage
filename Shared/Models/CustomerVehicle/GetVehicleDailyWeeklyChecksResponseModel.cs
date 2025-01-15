namespace Shared.Models.CustomerVehicle;

public class GetVehicleDailyWeeklyChecksResponseModel : GetVehicleChecksResponseModel
{
    public int Id { get; set; }
    public int StorageBayId { get; set; }
    public int CustomerStorageContractId { get; set; }
    public DateTime Date { get; set; }
}