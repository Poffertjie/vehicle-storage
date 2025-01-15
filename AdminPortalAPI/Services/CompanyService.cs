using AdminPortalAPI.Controllers;
using AutoWrapper.Wrappers;
using Database.DbContext;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Company;

namespace AdminPortalAPI.Services;

public class CompanyService : BaseService
{
    public CompanyService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }

    public async Task<GetCompanyResponseModel> GetCompany()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.Companies.Where(x => x.Id == loggedInUser.CompanyId).Select(x =>
                new GetCompanyResponseModel
                {
                    CompanyName = x.Name,
                    Address = x.Address,
                    PhoneNumber = x.ContactNumber
                }).FirstOrDefaultAsync() ?? new();
        });
    }

    public async Task<bool> UpdateCompany(UpdateCompanyResponseModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var company = await _context.Companies.FirstOrDefaultAsync(x => x.Id == loggedInUser.CompanyId);

            if(company is null)
                throw new ApiException("Company not found");

            company.Name = model.CompanyName;
            company.Address = model.Address;
            company.ContactNumber = model.PhoneNumber;
            return true;
        });
    }
    
}