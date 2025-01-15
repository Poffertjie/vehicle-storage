namespace Shared.Models.Customer;

public class GetCustomerResponseModel
{
    public UpdateCustomerDetails UpdateCustomerDetails { get; set; }
    public IEnumerable<GetCustomerContractGridResponse> CustomerContracts { get; set; }
    public IEnumerable<GetCustomerVehicleGridResponse> CustomerVehicles { get; set; }
}