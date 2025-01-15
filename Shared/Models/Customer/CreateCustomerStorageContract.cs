using Shared.Enums;

namespace Shared.Models.Customer;

public class CreateCustomerStorageContract
{
    public CreateCustomerStorageContract(int? customerId, int? customerVehicleId)
    {
        if (CustomerId.HasValue)
        {
            CustomerId = customerId;

            if (CustomerVehicleId.HasValue)
                CustomerVehicleId = customerVehicleId;
            else
                CustomerVehicle = new();
        }
        else
        {
            Customer = new();
            AdditionalContactPersons = new();
            CustomerVehicle = new();
        }
    }
    public int? CustomerId { get; set; }
    public int? CustomerVehicleId { get; set; }
    public CreateCustomer? Customer { get; set; }
    public CreateCustomerVehicle? CustomerVehicle { get; set; }
    public CreateCustomerContract CustomerContract { get; set; }= new();
    public List<CreateAdditionalContactPerson>? AdditionalContactPersons { get; set; }
}
