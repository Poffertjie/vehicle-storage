namespace Shared.Models.Vehicles;

public class CreateVehicleRequestModel
{
    public int BrandId { get; set; }
    public int BrandModelId { get; set; }
    public int? BrandModelVariantId { get; set; }
    public int FuelTypeId { get; set; }
    public string ShortDescription { get; set; }
}