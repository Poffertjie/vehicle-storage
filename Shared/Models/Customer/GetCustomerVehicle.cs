namespace Shared.Models.Customer;

public class GetCustomerVehicle : UpdateCustomerVehicle
{
    public int BrandId { get; set; }
    public int BrandModelId { get; set; }
    public int? BrandModelVariantId { get; set; }
}