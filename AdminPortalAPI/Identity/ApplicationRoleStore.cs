using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPortalAPI.Identity
{
    public class ApplicationRoleStore : IRoleStore<ApplicationRole>
    {
        private readonly VehicleManagementContext _context;

        public ApplicationRoleStore(VehicleManagementContext Context)
        {
            _context = Context;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));

            Role? newRole = role.ToRole();
            newRole.Id = Convert.ToString(Guid.NewGuid());
            await _context.Roles.AddAsync(newRole);
            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
                return IdentityResult.Success;
            else
                return IdentityResult.Failed(new IdentityError { Description = $"Could not insert role {role.Name}." });
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            Role? existingRole = role.ToRole();
            _context.Roles.Attach(existingRole);
            _context.Entry(existingRole).State = EntityState.Modified;
            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
                return IdentityResult.Success;
            return IdentityResult.Failed(new IdentityError { Description = $"Failed to update role {role.Name}." });
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (roleId == null) throw new ArgumentNullException(nameof(roleId));

            Role? role = await _context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);

            if (role != null)
                return new ApplicationRole(role);

            return null;
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedRoleName == null) throw new ArgumentNullException(nameof(normalizedRoleName));

            Role? role = await _context.Roles.FirstOrDefaultAsync(x => x.Name == normalizedRoleName);

            if (role != null)
                return new ApplicationRole(role);

            return null;
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Name);
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(Convert.ToString(role.Id));
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));

            role.Name = normalizedName;
            return Task.FromResult<object>(null);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (role == null) throw new ArgumentNullException(nameof(role));
            if (roleName == null) throw new ArgumentNullException(nameof(roleName));
            role.Name = roleName;
            return Task.FromResult<object>(null);
        }


    }
}
