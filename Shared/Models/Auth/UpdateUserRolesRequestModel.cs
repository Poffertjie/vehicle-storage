namespace Shared.Models.Auth;

public class UpdateUserRolesRequestModel
{
    public string UserId { get; set; }
    public List<string> AssignedRoles { get; set; } = new();
    public List<string> UnassignedRoles { get; set; } = new();
}
