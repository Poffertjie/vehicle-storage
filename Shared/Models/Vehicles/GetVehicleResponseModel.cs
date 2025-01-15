namespace Shared.Models.Vehicles;

public class GetVehicleResponseModel : UpdateVehicleRequestModel
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string BrandModel { get; set; }
    public string? BrandModelVariant { get; set; }
    public string FuelType { get; set; }
}