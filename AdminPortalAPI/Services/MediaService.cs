using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Media;

namespace AdminPortalAPI.Services;

public class MediaService : BaseService
{
    public MediaService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context,
        httpContextAccessor)
    {
    }

    public async Task<string> GetMedia(string id)
    {
        return await QueryData(async () =>
        {
            var media = await _context.Media.FirstOrDefaultAsync(x => x.Id == id);
            return media != null ? media.File : "";
        });
    }

    public async Task<string> CreateMedia(CreateMediaRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Media.AddAsync(new Medium()
            {
                Id = Guid.NewGuid().ToString(),
                CompanyId = loggedInUser.CompanyId,
                Name = model.Name,
                File = model.File,
                ContentType = model.ContentType,
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add Brand model variant");

            return entity.Entity.Id;
        });
    }

    public async Task<bool> UpdateMedia(UpdateMediaRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Media.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is null)
                throw new ApiException("Failed to media to update");

            entity.Name = model.Name;
            entity.File = model.File;
            entity.ContentType = model.ContentType;

            return true;
        });
    }

    public async Task<bool> DeleteMedia(string id)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Media.FirstOrDefaultAsync(x => x.Id == id);

            if (entity is not null)
                _context.Media.Remove(entity);
            return true;
        });
    }
}