namespace Shared.Models.Auth;

public class GetUsersAndRolesResponseModel
{
    public string UserId { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public List<string> Assigned { get; set; }
    public List<string> Unassigned { get; set; }
}