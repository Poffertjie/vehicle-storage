using Microsoft.AspNetCore.Authorization;
using Shared.Enums;

namespace AdminPortalAPI.Attributes;

public class AuthorizeRolesAttribute : AuthorizeAttribute
{
    public AuthorizeRolesAttribute(params RolesEnum[] roles) : base()
    {
        Roles = string.Join(",", roles);
    }
}