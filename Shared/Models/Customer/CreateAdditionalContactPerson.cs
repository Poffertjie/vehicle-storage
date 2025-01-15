using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Customer;

public class CreateAdditionalContactPerson
{
    public int Id { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Full Name is required")]
    public string FullName { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Identification Number is required")]
    public string IdentificationNumber { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Contact Number is required")]
    public string ContactNumber { get; set; }
    [Required(AllowEmptyStrings = false, ErrorMessage = "Relationship is required")]
    public string Relationship { get; set; }
}