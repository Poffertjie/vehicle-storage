namespace Shared.Models.CustomerVehicle;

public class EndCustomerVehicleCheckOutRequestModel
{
    public int Id { get; set; }
    public int CustomerVehicleId { get; set; }
    public bool ReturnAllBelongings { get; set; }

    public bool ReturnNumberPlates { get; set; }
    public string ErrorCodeImage { get; set; }
}