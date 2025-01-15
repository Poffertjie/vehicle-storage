namespace Shared.Models.Customer;

public class UpdateCustomerDetails
{
    public UpdateCustomer Customer { get; set; } = new();
    public List<UpdateAdditionalContactPerson> AdditionalContactPersons { get; set; } = new();
}