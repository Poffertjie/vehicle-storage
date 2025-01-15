using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;
using Shared.Models.ServiceAdvisor;

namespace AdminPortalAPI.Services;

public class ServiceAdvisorService : BaseService
{
    public ServiceAdvisorService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }

    public async Task<IEnumerable<GetServiceAdvisorResponseModel>> GetServiceAdvisors()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.ServiceAdvisors.Where(x => x.CompanyId == loggedInUser.CompanyId).Select(x => new GetServiceAdvisorResponseModel
            {
                Id = x.Id,
                Address = x.Address,
                ContactNumber = x.ContactNumber,
                Name = x.Name,
            }).ToListAsync();
        });
    }
    
    public async Task<bool> CreateServiceAdvisor(CreateServiceAdvisorRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.ServiceAdvisors.AddAsync(new ServiceAdvisor()
            {
                CompanyId = loggedInUser.CompanyId,
                Name = model.Name,
                Address = model.Address,
                ContactNumber = model.ContactNumber,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add service advisor");


            return true;
        });
    }

    public async Task<bool> UpdateServiceAdvisor(UpdateServiceAdvisorRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.ServiceAdvisors.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is not null)
            {
                entity.Name = model.Name;
                entity.Address = model.Address;
                entity.ContactNumber = model.ContactNumber;
                entity.MODIFIED_BY = loggedInUser.UserId;
            }

            return true;
        });
    }
}