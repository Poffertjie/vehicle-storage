namespace Shared.Models.CustomerVehicle;

public class StartCustomerVehicleCheckOutRequestModel
{
    public int CustomerStorageContractId { get; set; }
    public int CustomerVehicleId { get; set; }
    public DateTime? CheckOutDate { get; set; }

    public DateTime? PlannedCheckInDate { get; set; }

    public int DeliveryMethodId { get; set; }

    public string? Address { get; set; }
}