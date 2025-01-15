using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Vehicles;

public class CreateBrandRequestModel
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Logo { get; set; }
}