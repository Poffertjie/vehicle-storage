namespace Shared.Models.Auth;

public class LoggedInUser
{
    public Guid UserId { get; set; }
    public int CompanyId { get; set; }
}