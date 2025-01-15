namespace Shared.Models.Customer;

public class GetCustomerGridResponseModel
{
    public int CustomerId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string ContactNumber { get; set; } = null!;
    public int CustomerStorageContractId { get; set; }
    public int CustomerVehicleId { get; set; }
    public string LicensePlate { get; set; }
    public string ChassisVin { get; set; }
    public string Vehicle { get; set; }
    public decimal VehicleValuation { get; set; }
    public DateTime ContractExpiry { get; set; }
    public DateTime NextService { get; set; }
    public string ServiceAdvisor { get; set; }
    public DateTime RegistrationExpiryDate { get; set; }
    public int StatusId { get; set; }
    public string Status { get; set; }
    public int StorageBayId { get; set; }
    public int StorageBayNumber { get; set; }
}