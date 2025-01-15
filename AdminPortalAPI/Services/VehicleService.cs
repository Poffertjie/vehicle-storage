using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;
using Shared.Models.Vehicles;

namespace AdminPortalAPI.Services;

public class VehicleService : BaseService
{
    public VehicleService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(context,
        httpContextAccessor)
    {
    }

    public async Task<int> GetVehicleId(int brandId, int brandModelId, int? brandModelVariantId)
    {
        return await QueryData(async () =>
        {
            var vehicle = await _context.Vehicles.FirstOrDefaultAsync(x => x.BrandId == brandId && x.BrandModelId == brandModelId && x.BrandModelVariantId == brandModelVariantId);
            return vehicle?.Id ?? 0;
        });
    }

    public async Task<IEnumerable<GetVehicleResponseModel>> GetVehicles()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.Vehicles.Where(x => x.CompanyId == loggedInUser.CompanyId).Select(x =>
                new GetVehicleResponseModel
                {
                    Id = x.Id,
                    BrandId = x.BrandId,
                    Brand = x.Brand.Name,
                    BrandModelId = x.BrandModelId,
                    BrandModel = x.BrandModel.Name,
                    BrandModelVariantId = x.BrandModelVariantId,
                    BrandModelVariant = x.BrandModelVariant != null ? x.BrandModelVariant.Name : null,
                    FuelTypeId = x.FuelTypeId,
                    FuelType = x.FuelType.Name
                }).ToListAsync();
        });
    }

    public async Task<bool> CreateVehicle(CreateVehicleRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Vehicles.AddAsync(new Vehicle()
            {
                CompanyId = loggedInUser.CompanyId,
                Description = model.ShortDescription,
                BrandId = model.BrandId,
                BrandModelId = model.BrandModelId,
                BrandModelVariantId = model.BrandModelVariantId,
                FuelTypeId = model.FuelTypeId,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add vechile");


            return true;
        });
    }

    public async Task<bool> UpdateVehicle(UpdateVehicleRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Vehicles.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is not null)
            {
                entity.BrandId = model.BrandId;
                entity.BrandModelId = model.BrandModelId;
                entity.BrandModelVariantId = model.BrandModelVariantId;
                entity.FuelTypeId = model.FuelTypeId;
                entity.MODIFIED_BY = loggedInUser.UserId;
            }

            return true;
        });
    }

    public async Task<IEnumerable<GetBrandResponseModel>> GetBrands()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.Brands.Where(x => x.CompanyId == loggedInUser.CompanyId).Select(x =>
                new GetBrandResponseModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToListAsync();
        });
    }


    public async Task<bool> CreateBrand(CreateBrandRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Brands.AddAsync(new Brand
            {
                CompanyId = loggedInUser.CompanyId,
                LogoFile = model.Logo,
                Name = model.Name,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add Brand");


            return true;
        });
    }

    public async Task<bool> UpdateBrand(UpdateBrandRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.Brands.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is not null)
            {
                entity.Name = model.Name;
                entity.LogoFile = model.Logo;
                entity.MODIFIED_BY = loggedInUser.UserId;
            }

            return true;
        });
    }

    public async Task<IEnumerable<GetBrandModelResponseModel>> GetBrandModels(int brandId)
    {
        return await QueryData(async () =>
        {
            return await _context.BrandModels.Where(x => x.BrandId == brandId).Select(x =>
                new GetBrandModelResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BrandId = x.BrandId
                }).ToListAsync();
        });
    }


    public async Task<bool> CreateBrandModel(CreateBrandModelRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.BrandModels.AddAsync(new BrandModel()
            {
                CompanyId = loggedInUser.CompanyId,
                Name = model.Name,
                BrandId = model.BrandId,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add Brand model");


            return true;
        });
    }

    public async Task<bool> UpdateBrandModel(UpdateBrandModelRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.BrandModels.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is not null)
            {
                entity.Name = model.Name;
                entity.BrandId = model.BrandId;
                entity.MODIFIED_BY = loggedInUser.UserId;
            }

            return true;
        });
    }

    public async Task<IEnumerable<GetBrandModelVariantResponseModel>> GetBrandModelVariants(int brandModelId)
    {
        return await QueryData(async () =>
        {
            return await _context.BrandModelVariants.Where(x => x.BrandModelId == brandModelId).Select(x =>
                new GetBrandModelVariantResponseModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    BrandModelId = x.BrandModelId
                }).ToListAsync();
        });
    }

    public async Task<bool> CreateBrandModelVariant(CreateBrandModelVariantRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.BrandModelVariants.AddAsync(new BrandModelVariant()
            {
                CompanyId = loggedInUser.CompanyId,
                Name = model.Name,
                BrandModelId = model.BrandModelId,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to add Brand model variant");


            return true;
        });
    }

    public async Task<bool> UpdateBrandModelVariant(UpdateBrandModelVariantRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.BrandModelVariants.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is not null)
            {
                entity.Name = model.Name;
                entity.BrandModelId = model.BrandModelId;
                entity.MODIFIED_BY = loggedInUser.UserId;
            }

            return true;
        });
    }
}