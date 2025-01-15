using System.ComponentModel.DataAnnotations;

namespace Shared.Models.Auth;

public class CreateUserRequestModel
{
    [Required(AllowEmptyStrings = false)]
    public string FullName { get; set; }
    [Required(AllowEmptyStrings = false), EmailAddress]
    public string Email { get; set; }
    [Required]
    [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
    public string Password { get; set; }

    [Required] [Compare(nameof(Password))] 
    public string ConfirmPassword { get; set; }
    public List<string> Roles { get; set; } = new();
}