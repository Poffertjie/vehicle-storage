using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Customer;

public class CreateCustomerContract
{
    [Required(ErrorMessage = "Next Service Date required"), DataType(DataType.DateTime)]
    public DateTime? StartDate { get; set; }
    [Required(ErrorMessage = "Next Service Date required"), DataType(DataType.DateTime)]
    public DateTime? EndDate { get; set; }
    [Range(1, double.MaxValue, ErrorMessage = "Please enter a price per month")]
    public decimal PricePerMonth { get; set; }
    public bool IsChargerSupplied { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please select a payment option")]
    public int PaymentOptionId { get; set; }
    [Range(1, int.MaxValue, ErrorMessage = "Please select a storage bay")]
    public int StorageBayId { get; set; }
}