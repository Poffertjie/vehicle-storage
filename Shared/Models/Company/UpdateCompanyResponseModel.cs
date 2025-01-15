using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Company;

public class UpdateCompanyResponseModel
{
    [Required(AllowEmptyStrings = false)]
    public string CompanyName { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Address { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string PhoneNumber { get; set; }
}