using Database.DbContext;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;

namespace AdminPortalAPI.Identity;

public class CompanyUserValidator : UserValidator<ApplicationUser>
{
    private readonly VehicleManagementContext _context;

    public CompanyUserValidator(VehicleManagementContext context)
    {
        _context = context;
    }

    public override async Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
    {
        var result = await base.ValidateAsync(manager, user);

        // Check if the email is unique within the company
        var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.CompanyId == user.CompanyId);
        if (existingUser != null)
        {
            var errors = result.Errors.ToList();
            errors.Add(new IdentityError { Description = "Email is already taken within the company." });
            return IdentityResult.Failed(errors.ToArray());
        }

        return result;
    }
}