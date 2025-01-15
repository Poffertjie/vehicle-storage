using Database.DbContext;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Customer;
using Shared.Models.Lookup;

namespace AdminPortalAPI.Services;

public class LookupService : BaseService
{
    public LookupService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }
    
    public async Task<IEnumerable<GetPaymentOptionsResponseModel>> GetPaymentOptions()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.PaymentOptions.Where(x => x.CompanyId == loggedInUser.CompanyId).Select(x =>
                new GetPaymentOptionsResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        });
    }
    
    public async Task<IEnumerable<GetDeliveryMethodsResponseModel>> GetDeliveryMethods()
    {
        return await QueryData(async () =>
        {
            return await _context.DeliveryMethods.Select(x =>
                new GetDeliveryMethodsResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        });
    }
}