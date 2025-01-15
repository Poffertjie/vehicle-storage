using System.Security.Claims;
using Database.DbContext;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Models.Auth;

namespace AdminPortalAPI.Services;

public abstract class BaseService
{
    protected readonly VehicleManagementContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BaseService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }


    protected async Task<T> UnitOfWork<T>(Func<Task<T>> function)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var response = await function();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return response;
        }
        catch (Exception)
        {
            await transaction.DisposeAsync();
            throw;
        }
    }
    
    protected async Task UnitOfWork(Func<Task> function)
    {
        var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await function();
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.DisposeAsync();
            throw;
        }
    }

    protected async Task<T> QueryData<T>(Func<Task<T>> function)
    {
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        await _context.Database.ExecuteSqlRawAsync("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        var response = await function();
        return response;
    }
    
    protected T QueryData<T>(Func<T> function)
    {
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking; 
        _context.Database.ExecuteSqlRaw("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
        var response =  function();
        return response;
    }

    protected LoggedInUser GetLoggedInUser()
    {
        var loggedInUser = new LoggedInUser();
        var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userIdClaim = identity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.UserData);

            if (userIdClaim is not null)
            {
                if (Guid.TryParse(userIdClaim.Value, out var userId))
                    loggedInUser.UserId = userId;
            }
            
            var companyIdClaim = identity.Claims.FirstOrDefault(x => x.Type == StringConstants.CompanyId);
            
            if (companyIdClaim is not null)
            {
                if (int.TryParse(companyIdClaim.Value, out var companyId))
                    loggedInUser.CompanyId = companyId;
            }
            
        }

        return loggedInUser;
    }
}