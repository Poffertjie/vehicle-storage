using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Models.Lookup;
using Shared.Models.Storage;

namespace AdminPortalAPI.Services;

public class StorageService : BaseService
{
    public StorageService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
    {
    }
    
    public async Task<IEnumerable<GetStorageBaysResponseModel>> GetStorageAllBays()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.StorageBays.Select(x =>
                new GetStorageBaysResponseModel
                {
                    Id = x.Id,
                    Number = x.Number,
                    Status = x.StorageBayStatus.Name
                }).ToListAsync();
        });
    }
    
    public async Task<IEnumerable<GetStorageBaysResponseModel>> GetStorageBays(StorageBayStatusEnum status)
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.StorageBays.Where(x => x.CompanyId == loggedInUser.CompanyId && x.StorageBayStatusId == (int)status).Select(x =>
                new GetStorageBaysResponseModel
                {
                    Id = x.Id,
                    Number = x.Number,
                }).ToListAsync();
        });
    }

    public async Task<bool> CreateStorageBay(CreateStorageBayRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            await _context.StorageBays.AddAsync(new StorageBay()
            {
                Number = model.Number,
                CompanyId = loggedInUser.CompanyId,
                MODIFIED_BY = loggedInUser.UserId,
                StorageBayStatusId = (int)StorageBayStatusEnum.Available
            });
            return true;
        });
    }
    
    public async Task<bool> UpdateStorageBay(UpdateStorageBayRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var storageBay = await _context.StorageBays.FirstOrDefaultAsync(x => x.Id == model.Id);
            
            if(storageBay is null)
                throw new ApiException("Storage Bay not found");
            
            storageBay.Number = model.Number;
            storageBay.MODIFIED_BY = loggedInUser.UserId;
            
            return true;
        });
    }

    public async Task<bool> StorageBayNumberExist(int number)
    {
        return await _context.StorageBays.AnyAsync(x => x.Number == number);
    }
}