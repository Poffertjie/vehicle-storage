namespace Shared.Models.Customer;

public class GetCustomerContractGridResponse
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal PricePerMonth { get; set; }
    public bool IsChargerSupplied { get; set; }
    public string PaymentMethod { get; set; }
    public int StorageBay { get; set; }
}