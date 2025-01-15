using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Auth;

public class LoginRequestModel
{
    [Required(AllowEmptyStrings = false), EmailAddress]
    public string Email { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}