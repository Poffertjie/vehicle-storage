using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Customer;

public class CreateCustomer
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
    public string FullName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
    public string Email { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Identification Number is required")]
    public string IdentificationNumber { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Identification File is required")]
    public string IdentificationFile { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Number is required")]
    public string ContactNumber { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
    public string Address { get; set; }
}