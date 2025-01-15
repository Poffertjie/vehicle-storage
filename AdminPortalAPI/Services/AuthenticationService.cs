using AdminPortalAPI.Helpers;
using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Models.Auth;

namespace AdminPortalAPI.Services;

public class AuthenticationService : BaseService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthenticationService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager) : base(context,
        httpContextAccessor)
    {
        _userManager = userManager;
    }

    public async Task<string?> Login(LoginRequestModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _context.UserRoles.Where(x => x.UserId == user.Id).Select(x => x.Role!.Name)
                .ToListAsync();
            return TokenBuilderHelper.BuildToken(user.Id, user.SystemAdmin, user.CompanyId, userRoles);
        }


        return null;
    }

    public async Task<bool> UpdateUserRoles(UpdateUserRolesRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is not null)
            {
                if (model.UnassignedRoles.Any())
                {
                    foreach (var role in model.UnassignedRoles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role))
                        {
                            var removeRoleRsult = await _userManager.RemoveFromRoleAsync(user, role);

                            if (!removeRoleRsult.Succeeded)
                                throw new ApiException($"Failed to remove role: {role} " );
                        }
                    }
                }

                if (model.AssignedRoles.Any())
                {
                    foreach (var role in model.AssignedRoles)
                    {
                        if (await _userManager.IsInRoleAsync(user, role) == false)
                        {
                            var addRoleResult = await _userManager.AddToRoleAsync(user, role);

                            if (!addRoleResult.Succeeded)
                                throw new ApiException($"Failed to assign role: {role}");
                        }
                    }
                }
            }


            return true;
        });
    }

    public async Task<bool> CreateUser(CreateUserRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var result = await _userManager.CreateAsync(new ApplicationUser()
            {
                FullName = model.FullName,
                Email = model.Email,
                CompanyId = loggedInUser.CompanyId
            }, model.Password);


            if (!result.Succeeded)
                throw new ApiException("Failed to create user");

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is not null)
            {
                var rolesResult = await _userManager.AddToRolesAsync(user, model.Roles);

                if (!rolesResult.Succeeded)
                    throw new ApiException("Failed to assign roles");
            }


            return true;
        });
    }

    public async Task<IEnumerable<GetUsersAndRolesResponseModel>> GetUsersAndRoles()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await (from user in _context.Users.Where(x => x.CompanyId == loggedInUser.CompanyId && x.UserRoles.All(ur => ur.Role.Name != RolesEnum.Customer.ToString()))
                select new GetUsersAndRolesResponseModel
                {
                    UserId = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    Assigned = user.UserRoles.Select(x => x.Role.Name).ToList(),
                    Unassigned = _context.Roles
                        .Where(role => user.UserRoles.All(ur => ur.RoleId != role.Id))
                        .Select(role => role.Name)
                        .ToList()
                }).ToListAsync();
        });
    }

    public async Task<IEnumerable<string>> GetRoles()
    {
        return await _context.Roles.Select(x => x.Name).ToListAsync();
    }
}