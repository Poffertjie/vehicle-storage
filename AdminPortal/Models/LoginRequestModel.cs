using System.ComponentModel.DataAnnotations;

namespace AdminPortal.Models;

public class LoginRequestModel
{
    [Required(AllowEmptyStrings = false), EmailAddress]
    public string Email { get; set; }
    [Required(AllowEmptyStrings = false)]
    public string Password { get; set; }
}