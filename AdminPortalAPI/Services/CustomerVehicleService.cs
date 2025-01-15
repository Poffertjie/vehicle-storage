using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shared.Enums;
using Shared.Models.Customer;
using Shared.Models.CustomerVehicle;

namespace AdminPortalAPI.Services;

public class CustomerVehicleService : BaseService
{
    public CustomerVehicleService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor) : base(
        context, httpContextAccessor)
    {
    }



    

    public async Task<IEnumerable<GetVehicleChecksResponseModel>> GetVehiclesForCheckIn() =>
        await GetVehiclesForChecks(VehicleStatusEnum.Checkin);

    public async Task<IEnumerable<GetVehicleForCheckoutResponseModel>> GetVehiclesForCheckOut() =>
        await GetVehiclesForCheckout();

    public async Task<IEnumerable<GetVehicleChecksResponseModel>> GetVehiclesCheckOut() =>
        await GetVehiclesForChecks(VehicleStatusEnum.CheckedOut);

    public async Task<IEnumerable<GetVehicleChecksResponseModel>> GetVehiclesForStorageCheckIn() =>
        await GetVehiclesForChecks(VehicleStatusEnum.StorageCheckin);

    public async Task<IEnumerable<GetVehicleChecksResponseModel>> GetVehiclesInStorage() =>
        await GetVehiclesForChecks(VehicleStatusEnum.Storage);

    private async Task<IEnumerable<GetVehicleChecksResponseModel>> GetVehiclesForChecks(VehicleStatusEnum status)
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.CustomerStorageContracts
                .Where(x => x.CustomerVehicle.StatusId == (int)status && x.CompanyId == loggedInUser.CompanyId).Select(
                    x =>
                        new GetVehicleChecksResponseModel
                        {
                            StorageBay = x.StorageBay.Number,
                            CustomerVehicleId = x.Id,
                            CustomerStorageContractId = x.Id,
                            Customer = x.Customer.User.FullName,
                            Brand = x.CustomerVehicle.Vehicle.Brand.Name,
                            Model = x.CustomerVehicle.Vehicle.BrandModel.Name,
                            LicensePlate = x.CustomerVehicle.LicensePlate,
                            ChassisVin = x.CustomerVehicle.ChassisVin
                        }).ToListAsync();
        });
    }

    private async Task<IEnumerable<GetVehicleForCheckoutResponseModel>> GetVehiclesForCheckout()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.CustomerVehicleCheckOuts.Where(x =>
                    x.CompanyId == loggedInUser.CompanyId &&
                    x.CustomerVehicle.StatusId == (int)VehicleStatusEnum.Checkout)
                .Select(x =>
                    new GetVehicleForCheckoutResponseModel
                    {
                        Id = x.Id,
                        CustomerStorageContractId = x.CustomerStorageContractId,
                        StorageBay = x.CustomerStorageContract.StorageBay.Number,
                        CustomerVehicleId = x.CustomerVehicleId,
                        Customer = x.CustomerVehicle.Customer.User.FullName,
                        Brand = x.CustomerVehicle.Vehicle.Brand.Name,
                        Model = x.CustomerVehicle.Vehicle.BrandModel.Name,
                        LicensePlate = x.CustomerVehicle.LicensePlate,
                        ChassisVin = x.CustomerVehicle.ChassisVin
                    }).ToListAsync();
        });
    }


    public async Task<bool> CustomerVehicleCheckIn(CustomerVehicleCheckInRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleCheckIns.AddAsync(new CustomerVehicleCheckIn
            {
                CustomerStorageContractId = model.CustomerStorageContractId,
                CustomerVehicleId = model.CustomerVehicleId,
                Detailing = model.Detailing,
                TyreMake = model.TyreMake,
                TyreYearModel = model.TyreYearModel,
                TyreTreadMeasure = model.TyreTreadMeasure,
                DeepWash = model.DeepWash,
                DiagnosticReportFile = model.DiagnosticReportFile,
                NumberPlateImage = model.NumberPlateImage,
                ChassisNumberImage = model.ChassisNumberImage,
                FrontImage = model.FrontImage,
                LeftFrontImage = model.LeftFrontImage,
                LeftFenderWheelImage = model.LeftFenderWheelImage,
                LeftDoorsImage = model.LeftDoorsImage,
                LeftRearFenderWheelImage = model.LeftRearFenderWheelImage,
                RearImage = model.RearImage,
                RightFrontImage = model.RightFrontImage,
                RightFenderWheelImage = model.RightFenderWheelImage,
                RightDoorsImage = model.RightDoorsImage,
                RightRearFenderWheelImage = model.RightRearFenderWheelImage,
                WarningLightsKilometers = model.WarningLightsKilometers,
                CompanyId = loggedInUser.CompanyId,
                TimeStamp = DateTime.Now,
                MODIFIED_BY = loggedInUser.UserId,
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to checkin customer vehicle");

            var customerVehicle =
                await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == model.CustomerVehicleId);

            if (customerVehicle is null)
                throw new ApiException("could not find customer vehicle to update status to storage check in");

            customerVehicle.StatusId = (int)VehicleStatusEnum.StorageCheckin;
            customerVehicle.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<bool> StartCustomerVehicleCheckOut(StartCustomerVehicleCheckOutRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleCheckOuts.AddAsync(new CustomerVehicleCheckOut
            {
                CustomerStorageContractId = model.CustomerStorageContractId,
                CustomerVehicleId = model.CustomerVehicleId,
                ReturnAllBelongings = false,
                ReturnNumberPlates = false,
                CheckOutDate = model.CheckOutDate.Value,
                PlannedCheckInDate = model.PlannedCheckInDate.Value,
                DeliveryMethodId = model.DeliveryMethodId,
                Address = model.Address,
                TimeStamp = DateTime.Now,
                CompanyId = loggedInUser.CompanyId,
                MODIFIED_BY = loggedInUser.UserId
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to start checkout customer vehicle");

            var customerVehicle =
                await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == model.CustomerVehicleId);

            if (customerVehicle is null)
                throw new ApiException("could not find customer vehicle to update status to check out");

            customerVehicle.StatusId = (int)VehicleStatusEnum.Checkout;
            customerVehicle.MODIFIED_BY = loggedInUser.UserId;
            return true;
        });
    }

    public async Task<bool> EndCustomerVehicleCheckOut(EndCustomerVehicleCheckOutRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleCheckOuts.FirstOrDefaultAsync(x =>
                x.Id == model.Id && x.CustomerVehicleId == model.CustomerVehicleId);

            if (entity is null)
                throw new ApiException("could not find customer vehicle checkout record to update");

            entity.ReturnAllBelongings = model.ReturnAllBelongings;
            entity.ReturnNumberPlates = model.ReturnNumberPlates;
            entity.ErrorCodeImage = model.ErrorCodeImage;
            entity.MODIFIED_BY = loggedInUser.UserId;

            var customerVehicle =
                await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == model.CustomerVehicleId);

            if (customerVehicle is null)
                throw new ApiException("could not find customer vehicle to update status to check out");

            customerVehicle.StatusId = (int)VehicleStatusEnum.CheckedOut;
            customerVehicle.MODIFIED_BY = loggedInUser.UserId;
            return true;
        });
    }

    public async Task<bool> CustomerVehicleStorageCheckIn(CustomerVehicleStorageCheckInRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleStorageCheckIns.AddAsync(new CustomerVehicleStorageCheckIn
            {
                CustomerStorageContractId = model.CustomerStorageContractId,
                CustomerVehicleId = model.CustomerVehicleId,
                VehicleOnChargeImage = model.VehicleOnChargeImage,
                KilometerReadingImage = model.KilometerReadingImage,
                VehicleUnderCoverImage = model.VehicleUnderCoverImage,
                ParkedTheVehicleOnTireCradles = model.ParkedTheVehicleOnTireCradles,
                ChargerConnected = model.ChargerConnected,
                SeatCover = model.SeatCover,
                SteeringCover = model.SteeringCover,
                FloorMats = model.FloorMats,
                KeyTagged = model.KeyTagged,
                KeyPlacedInSafe = model.KeyPlacedInSafe,
                CompanyId = loggedInUser.CompanyId,
                TimeStamp = DateTime.Now,
                MODIFIED_BY = loggedInUser.UserId,
            });

            if (entity.State != EntityState.Added)
                throw new ApiException("Failed to stroage checkin customer vehicle");

            var customerVehicle =
                await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == model.CustomerVehicleId);

            if (customerVehicle is null)
                throw new ApiException("could not find customer vehicle to update status to storage");

            customerVehicle.StatusId = (int)VehicleStatusEnum.Storage;
            customerVehicle.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<bool> StartCustomerVehicleCheckInAfterCheckedOutState(int customerVehicleId,
        int customerStorageContractId)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var customerCheckOut = await _context.CustomerVehicleCheckOuts.FirstOrDefaultAsync(x =>
                x.CustomerVehicleId == customerVehicleId && x.CustomerStorageContractId == customerStorageContractId &&
                x.CustomerVehicle.StatusId == (int)VehicleStatusEnum.CheckedOut);

            if (customerCheckOut is null)
                throw new ApiException("could not find customer vehicle checkout to update status to check out");

            customerCheckOut.CheckInDate = DateTime.Now;
            customerCheckOut.MODIFIED_BY = loggedInUser.UserId;
            
            var customerVehicle = await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == customerVehicleId);
            
            if (customerVehicle is null)
                throw new ApiException("could not find customer vehicle to update status to check out");
            
            customerVehicle.StatusId = (int)VehicleStatusEnum.Checkin;
            customerVehicle.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<IEnumerable<GetVehicleDailyWeeklyChecksResponseModel>> GetDailyChecks()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.CustomerVehicleDailyChecks
                .Where(x => x.CompanyId == loggedInUser.CompanyId && x.Completed == false)
                .Select(x => new GetVehicleDailyWeeklyChecksResponseModel
                {
                    Id = x.Id,
                    StorageBay = x.CustomerStorageContract.StorageBay.Number,
                    StorageBayId = x.CustomerStorageContract.StorageBayId,
                    CustomerStorageContractId = x.CustomerStorageContract.Id,
                    CustomerVehicleId = x.CustomerVehicle.Id,
                    Customer = x.CustomerVehicle.Customer.User.FullName,
                    Brand = x.CustomerVehicle.Vehicle.Brand.Name,
                    Model = x.CustomerVehicle.Vehicle.BrandModel.Name,
                    LicensePlate = x.CustomerVehicle.LicensePlate,
                    ChassisVin = x.CustomerVehicle.ChassisVin,
                    Date = x.Date
                }).ToListAsync();
        });
    }

    public async Task CreateDailyCheckEntries()
    {
        var data = await QueryData(async () =>
        {
            return await _context.CustomerStorageContracts
                .Where(x => x.CustomerVehicle.StatusId == (int)VehicleStatusEnum.Storage).Select(x =>
                    new
                    {
                        CustomerStorageContract = x.Id,
                        CustomerVehicle = x.CustomerVehicle.Id,
                        x.CompanyId
                    }).ToListAsync();
        });

        var companyId = data.FirstOrDefault()?.CompanyId;
        var today = DateTime.Now.Date;
        var serviceAccount = await _context.UserRoles
            .Where(x => x.User.CompanyId == companyId && x.Role.Name == RolesEnum.Serviceaccount.ToString())
            .Select(x => x.UserId).FirstOrDefaultAsync();
        await UnitOfWork(async () =>
        {
            await _context.CustomerVehicleDailyChecks.AddRangeAsync(data.Select(x => new CustomerVehicleDailyCheck
            {
                CustomerStorageContractId = x.CustomerStorageContract,
                CustomerVehicleId = x.CustomerVehicle,
                Completed = false,
                CompanyId = x.CompanyId,
                MODIFIED_BY = new Guid(),
                Date = today
            }));
        });
    }

    public async Task<GetCustomerVehicleDailyCheckResponseModel> GetDailyCheck(int id)
    {
        return (await QueryData(async () =>
        {
            return await _context.CustomerVehicleDailyChecks.Where(x => x.Id == id).Select(x =>
                new GetCustomerVehicleDailyCheckResponseModel
                {
                    Id = x.Id,
                    OilCheck = x.OilCheck ?? "",
                    TyreCheck = x.TyreCheck ?? "",
                    WaterCheck = x.WaterCheck ?? "",
                    BatteryChargeStatus = x.BatteryChargeStatus ?? "",
                }).FirstOrDefaultAsync();
        }))!;
    }

    public async Task<bool> CustomerVehicleDailyCheck(CustomerVehicleDailyCheckRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleDailyChecks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is null)
                throw new ApiException("Could not find customer vehicle for daily checks");

            entity.BatteryChargeStatus = model.BatteryChargeStatus;
            entity.TyreCheck = model.TyreCheck;
            entity.OilCheck = model.OilCheck;
            entity.WaterCheck = model.WaterCheck;
            entity.SpeedometerImage = model.SpeedometerImage;
            entity.Completed = true;
            entity.MODIFIED_BY = loggedInUser.UserId;


            var contract =
                await _context.CustomerStorageContracts.FirstOrDefaultAsync(
                    x => x.Id == model.CustomerStorageContractId);

            if (contract is null)
                throw new ApiException("Could not find customer contract for daily check storage bay update");

            contract.StorageBayId = model.StorageBayId;
            contract.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<IEnumerable<GetVehicleDailyWeeklyChecksResponseModel>> GetWeeklyChecks()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.CustomerVehicleWeeklyChecks
                .Where(x => x.CompanyId == loggedInUser.CompanyId && x.Completed == false)
                .Select(x => new GetVehicleDailyWeeklyChecksResponseModel
                {
                    Id = x.Id,
                    StorageBay = x.CustomerStorageContract.StorageBay.Number,
                    StorageBayId = x.CustomerStorageContract.StorageBayId,
                    CustomerStorageContractId = x.CustomerStorageContract.Id,
                    CustomerVehicleId = x.CustomerVehicle.Id,
                    Customer = x.CustomerVehicle.Customer.User.FullName,
                    Brand = x.CustomerVehicle.Vehicle.Brand.Name,
                    Model = x.CustomerVehicle.Vehicle.BrandModel.Name,
                    LicensePlate = x.CustomerVehicle.LicensePlate,
                    ChassisVin = x.CustomerVehicle.ChassisVin,
                    Date = x.Date
                }).ToListAsync();
        });
    }

    public async Task CreateWeeklyCheckEntries()
    {
        var data = await QueryData(async () =>
        {
            return await _context.CustomerStorageContracts
                .Where(x => x.CustomerVehicle.StatusId == (int)VehicleStatusEnum.Storage).Select(x =>
                    new
                    {
                        CustomerStorageContract = x.Id,
                        CustomerVehicle = x.CustomerVehicle.Id,
                        x.CompanyId
                    }).ToListAsync();
        });

        var companyId = data.FirstOrDefault()?.CompanyId;
        var today = DateTime.Now.Date;
        // var serviceAccount = await _context.UserRoles
        //     .Where(x => x.User.CompanyId == companyId && x.Role.Name == RolesEnum.ServiceAccount.ToString())
        //     .Select(x => x.UserId).FirstOrDefaultAsync();
        await UnitOfWork(async () =>
        {
            await _context.CustomerVehicleWeeklyChecks.AddRangeAsync(data.Select(x => new CustomerVehicleWeeklyCheck
            {
                CustomerStorageContractId = x.CustomerStorageContract,
                CustomerVehicleId = x.CustomerVehicle,
                CompanyId = x.CompanyId,
                MODIFIED_BY = new Guid(),
                Completed = false,
                Date = today
            }));
        });
    }

    public async Task<GetCustomerVehicleWeeklyCheckResponseModel> GetWeeklyCheck(int id)
    {
        return (await QueryData(async () =>
        {
            return await _context.CustomerVehicleWeeklyChecks.Where(x => x.Id == id).Select(x =>
                new GetCustomerVehicleWeeklyCheckResponseModel
                {
                    Id = x.Id,
                    BatteryChargeStatus = x.BatteryChargeStatus ?? "",
                    BatteryStatus = x.BatteryStatus ?? "",
                    TirePressureFrontLeft = x.TirePressureFrontLeft ?? "",
                    TirePressureFrontRight = x.TirePressureFrontRight ?? "",
                    TirePressureRearLeft = x.TirePressureRearLeft ?? "",
                    TirePressureRearRight = x.TirePressureRearRight ?? "",
                }).FirstOrDefaultAsync();
        }))!;
    }

    public async Task<bool> CustomerVehicleWeeklyCheck(CustomerVehicleWeeklyCheckRequestModel model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicleWeeklyChecks.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is null)
                throw new ApiException("Could not find customer vehicle for weekly checks");

            entity.BatteryChargeStatus = model.BatteryChargeStatus;
            entity.BatteryStatus = model.BatteryStatus;
            entity.TirePressureFrontRight = model.TirePressureFrontRight;
            entity.TirePressureRearRight = model.TirePressureRearRight;
            entity.TirePressureRearLeft = model.TirePressureRearLeft;
            entity.TirePressureFrontLeft = model.TirePressureFrontLeft;
            entity.Completed = true;
            entity.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<GetCustomerVehicle> GetCustomerVehicleForUpdate(int customerVehicleId)
    {
        return await QueryData(async () =>
        {
            return await _context.CustomerVehicles.Where(x => x.Id == customerVehicleId).Select(x =>
                new GetCustomerVehicle
                {
                    LicensePlate = x.LicensePlate,
                    ChassinVin = x.ChassisVin,
                    VehicleId = x.VehicleId,
                    AverageVehicleValuation = x.AverageVehicleValuation,
                    NextServiceDate = x.NextService,
                    RegistrationExpiryDate = x.RegistrationExpiryDate,
                    RegistrationFile = x.VehicleRegistrationFile,
                    ServiceAdvisorId = x.ServiceAdvisorId,
                    BrandId = x.Vehicle.BrandId,
                    BrandModelId = x.Vehicle.BrandModelId,
                    BrandModelVariantId = x.Vehicle.BrandModelVariantId,
                    Id = x.Id
                }).FirstOrDefaultAsync() ?? new();
        });
    }

    public async Task<bool> UpdateCustomerVehicle(UpdateCustomerVehicle model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerVehicles.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is null)
                throw new ApiException("Could not find customer vehicle for update");

            entity.LicensePlate = model.LicensePlate;
            entity.ChassisVin = model.ChassinVin;
            entity.VehicleId = model.VehicleId;
            entity.AverageVehicleValuation = model.AverageVehicleValuation;
            entity.NextService = model.NextServiceDate.Value;
            entity.RegistrationExpiryDate = model.RegistrationExpiryDate.Value;
            entity.ServiceAdvisorId = model.ServiceAdvisorId;
            entity.VehicleRegistrationFile = model.RegistrationFile;
            entity.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }

    public async Task<UpdateCustomerContract> GetCustomerContractForUpdate(int customerContractId)
    {
        return await QueryData(async () =>
        {
            return await _context.CustomerStorageContracts.Where(x => x.Id == customerContractId).Select(x =>
                new UpdateCustomerContract
                {
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    PricePerMonth = x.PricePerMonth,
                    IsChargerSupplied = x.IsChargerSupplied,
                    PaymentOptionId = x.PaymentOptionId,
                    StorageBayId = x.StorageBayId,
                    Id = x.Id
                }).FirstOrDefaultAsync() ?? new();
        });
    }

    public async Task<bool> UpdateCustomerContract(UpdateCustomerContract model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            var entity = await _context.CustomerStorageContracts.FirstOrDefaultAsync(x => x.Id == model.Id);

            if (entity is null)
                throw new ApiException("Could not find customer contract for update");

            entity.StartDate = model.StartDate.Value;
            entity.EndDate = model.EndDate.Value;
            entity.PricePerMonth = model.PricePerMonth;
            entity.IsChargerSupplied = model.IsChargerSupplied;
            entity.PaymentOptionId = model.PaymentOptionId;
            entity.StorageBayId = model.StorageBayId;
            entity.MODIFIED_BY = loggedInUser.UserId;

            return true;
        });
    }
}