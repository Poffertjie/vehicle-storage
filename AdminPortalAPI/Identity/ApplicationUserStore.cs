using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AdminPortalAPI.Identity
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>, IUserSecurityStampStore<ApplicationUser>,
        IUserEmailStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly VehicleManagementContext _context;

        public ApplicationUserStore(VehicleManagementContext Context)
        {
            _context = Context;
        }

        public void Dispose()
        {
        }

        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task SetUserNameAsync(ApplicationUser user, string? userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (userName == null) throw new ArgumentNullException(nameof(userName));
            user.Email = userName;
            return Task.FromResult<object>(null);
        }

        public Task<string?> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string? normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedName == null) throw new ArgumentNullException(nameof(normalizedName));
            user.Email = normalizedName;
            return Task.FromResult<object>(null);
        }

        public async Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var newUser = user.ToUser();
            newUser.Id = Convert.ToString(Guid.NewGuid());
            await _context.Users.AddAsync(newUser);
            var rows = await _context.SaveChangesAsync();
            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            var existingUser = user.ToUser();

            //detach entity
            var local = _context.Set<User>().Local.FirstOrDefault(x => x.Id == existingUser.Id);
            if (local != null)
                _context.Entry(local).State = EntityState.Detached;

            //attach entity
            _context.Users.Attach(existingUser);
            _context.Entry(existingUser).State = EntityState.Modified;
            var rows = await _context.SaveChangesAsync();
            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));

            var existingUser = user.ToUser();
            _context.Users.Attach(existingUser);
            _context.Entry(existingUser).State = EntityState.Modified;
            var rows = await _context.SaveChangesAsync();

            if (rows > 0)
                return IdentityResult.Success;

            return IdentityResult.Failed();
        }

        public async Task<ApplicationUser?> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (userId == null) throw new ArgumentNullException(nameof(userId));

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);

            if (user != null)
                return new ApplicationUser(user);

            return null;
        }

        public async Task<ApplicationUser?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (normalizedUserName == null) throw new ArgumentNullException(nameof(normalizedUserName));

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == normalizedUserName);

            if (user != null)
                return new ApplicationUser(user);

            return null;
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string? passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (passwordHash == null) throw new ArgumentNullException(nameof(passwordHash));
            user.PasswordHash = passwordHash;
            return Task.FromResult<object>(null);
        }

        public Task<string?> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetSecurityStampAsync(ApplicationUser user, string stamp, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (stamp == null) throw new ArgumentNullException(nameof(stamp));
            user.SecurityStamp = stamp;
            return Task.FromResult<object>(null);
        }

        public Task<string?> GetSecurityStampAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.SecurityStamp);
        }

        public Task SetEmailAsync(ApplicationUser user, string? email, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (email == null) throw new ArgumentNullException(nameof(email));
            user.Email = email;
            return Task.FromResult<object>(null);
        }

        public Task<string?> GetEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUser user, bool confirmed, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (confirmed == null) throw new ArgumentNullException(nameof(confirmed));
            user.EmailConfirmed = confirmed;
            return Task.FromResult<object>(null);
        }

        public async Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == normalizedEmail);

            if (user is not null)
                return new ApplicationUser(user);

            return null;
        }

        public Task<string?> GetNormalizedEmailAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedEmailAsync(ApplicationUser user, string? normalizedEmail, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (normalizedEmail == null) throw new ArgumentNullException(nameof(normalizedEmail));
            user.Email = normalizedEmail;
            return Task.FromResult<object>(null);
        }
        
        //user roles
        public Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = _context.Roles.FirstOrDefault(x => x.Name == roleName).Id;
            _context.UserRoles.Add(new UserRole
            {
                UserId = user.Id,
                RoleId = roleId,
                MODIFIED_BY = Guid.Parse(user.Id)
            });
            _context.SaveChanges();
            return Task.FromResult<object>(null);
        }

        public Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = _context.Roles.FirstOrDefault(x => x.Name == roleName).Id;

            if (_context.UserRoles.Count(x => x.RoleId == roleId && x.UserId == user.Id) > 0)
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            var roleId = _context.Roles.FirstOrDefault(x => x.Name == roleName).Id;
            var userRole = _context.UserRoles.FirstOrDefault(x => x.RoleId == roleId);

            if (userRole is not null)
            {
                _context.UserRoles.Remove(userRole);
                _context.SaveChanges();
            }
     
            return Task.FromResult<object>(null);
        }

        public Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            IList<ApplicationUser> list = (from roles in _context.Roles
                                         join userRoles in _context.UserRoles on roles.Id equals userRoles.RoleId
                                         join users in _context.Users on userRoles.UserId equals users.Id
                                         select new ApplicationUser(users)).ToList();

            return Task.FromResult(list);
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            IList<string> list = (from userRoles in _context.UserRoles
                                  join roles in _context.Roles on userRoles.RoleId equals roles.Id
                                  where userRoles.UserId == user.Id
                                  select roles.Name).ToList();

            return Task.FromResult(list);
        }
    }
}
