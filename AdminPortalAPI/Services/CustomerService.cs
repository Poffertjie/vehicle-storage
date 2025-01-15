using AutoWrapper.Wrappers;
using Database.DbContext;
using Database.DbContext.DbModels;
using Database.DbContext.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using Shared.Models.Customer;

namespace AdminPortalAPI.Services;

public class CustomerService : BaseService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public CustomerService(VehicleManagementContext context, IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager) : base(context, httpContextAccessor)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<GetCustomerGridResponseModel>> GetCustomers()
    {
        return await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            return await _context.CustomerStorageContracts.Where(x => x.CompanyId == loggedInUser.CompanyId).Select(x =>
                new GetCustomerGridResponseModel
                {
                    FullName = x.Customer.User.FullName,
                    ContactNumber = x.Customer.ContactNumber,
                    Vehicle = x.CustomerVehicle.Vehicle.Description,
                    NextService = x.CustomerVehicle.NextService,
                    RegistrationExpiryDate = x.CustomerVehicle.RegistrationExpiryDate,
                    Status = x.CustomerVehicle.Status.Status,
                    StatusId = x.CustomerVehicle.StatusId,
                    ContractExpiry = x.EndDate,
                    StorageBayNumber = x.StorageBay.Number,
                    StorageBayId = x.StorageBay.Id,
                    CustomerVehicleId = x.CustomerVehicleId,
                    CustomerStorageContractId = x.Id,
                    CustomerId = x.CustomerId,
                    LicensePlate = x.CustomerVehicle.LicensePlate,
                    ChassisVin = x.CustomerVehicle.ChassisVin,
                    VehicleValuation = x.CustomerVehicle.AverageVehicleValuation,
                    ServiceAdvisor = x.CustomerVehicle.ServiceAdvisor.Name
                }).ToListAsync();
        });
    }


    public async Task<GetCustomerResponseModel> GetCustomer(int customerId)
    {
        var response = new GetCustomerResponseModel();
        await QueryData(async () =>
        {
            var loggedInUser = GetLoggedInUser();
            response.UpdateCustomerDetails = await _context.Customers
                .Where(x => x.CompanyId == loggedInUser.CompanyId && x.Id == customerId).Select(x =>
                    new UpdateCustomerDetails
                    {
                        Customer = new UpdateCustomer
                        {
                            Id = x.Id,
                            FullName = x.User.FullName,
                            Email = x.User.Email,
                            IdentificationNumber = x.IdentificationNumber,
                            IdentificationFile = x.IdentificationFile,
                            ContactNumber = x.ContactNumber,
                            Address = x.Address,
                        },
                        AdditionalContactPersons = x.AdditionalContactPeople.Select(a =>
                            new UpdateAdditionalContactPerson
                            {
                                Relationship = a.Relationship,
                                ContactNumber = a.ContactNumber,
                                FullName = a.FullName,
                                IdentificationNumber = a.IdentificationNumber,
                                Id = a.Id,
                            }).ToList()
                    }).FirstOrDefaultAsync() ?? new();

            response.CustomerContracts = await _context.CustomerStorageContracts.Where(x => x.CustomerId == customerId)
                .Select(x =>
                    new GetCustomerContractGridResponse
                    {
                        Id = x.Id,
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        PricePerMonth = x.PricePerMonth,
                        IsChargerSupplied = x.IsChargerSupplied,
                        PaymentMethod = x.PaymentOption.Name,
                        StorageBay = x.StorageBay.Number
                    }).ToListAsync();

            response.CustomerVehicles = await _context.CustomerStorageContracts.Where(x => x.CustomerId == customerId)
                .Select(
                    x => new GetCustomerVehicleGridResponse
                    {
                        CustomerStorageContractId = x.Id,
                        CustomerVehicleId = x.CustomerVehicleId,
                        LicensePlate = x.CustomerVehicle.LicensePlate,
                        ChassisVin = x.CustomerVehicle.ChassisVin,
                        Vehicle = x.CustomerVehicle.Vehicle.Description,
                        VehicleValuation = x.CustomerVehicle.AverageVehicleValuation,
                        NextService = x.CustomerVehicle.NextService,
                        ServiceAdvisor = x.CustomerVehicle.ServiceAdvisor.Name,
                        RegistrationExpiryDate = x.CustomerVehicle.RegistrationExpiryDate,
                        StatusId = x.CustomerVehicle.StatusId,
                        Status = x.CustomerVehicle.Status.Status,
                        StorageBayId = x.StorageBayId,
                        StorageBayNumber = x.StorageBay.Number
                    }).ToListAsync();
        });

        return response;
    }

    public async Task<bool> CreateCustomerContract(CreateCustomerStorageContract model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInuser = GetLoggedInUser();

            var customerId = 0;
            if (model.CustomerId <= 0)
            {
                var result = await _userManager.CreateAsync(new ApplicationUser()
                {
                    FullName = model.Customer.FullName,
                    Email = model.Customer.Email,
                    CompanyId = loggedInuser.CompanyId
                }, "Customer@123");

                if (!result.Succeeded)
                    throw new ApiException("Failed to create customer.");

                var user = await _userManager.FindByEmailAsync(model.Customer.Email);

                var roleResult = await _userManager.AddToRoleAsync(user, RolesEnum.Customer.ToString());
                if (!roleResult.Succeeded)
                    throw new ApiException("Failed to assign customer role");

                var customer = await _context.Customers.AddAsync(new Customer()
                {
                    CompanyId = loggedInuser.CompanyId,
                    UserId = user.Id,
                    IdentificationNumber = model.Customer.IdentificationNumber,
                    IdentificationFile = model.Customer.IdentificationFile,
                    Address = model.Customer.Address,
                    ContactNumber = model.Customer.ContactNumber,
                    AdditionalContactPeople = model.AdditionalContactPersons.Select(x =>
                        new AdditionalContactPerson()
                        {
                            CompanyId = loggedInuser.CompanyId,
                            IdentificationNumber = x.IdentificationNumber,
                            FullName = x.FullName,
                            ContactNumber = x.ContactNumber,
                            Relationship = x.Relationship
                        }).ToList(),
                    MODIFIED_BY = loggedInuser.UserId,
                });

                await _context.SaveChangesAsync();

                customerId = customer.Entity.Id;
            }
            else
                customerId = model.CustomerId.Value;


            var customerVehicleId = 0;

            if (model.CustomerVehicleId <= 0)
            {
                var customerVehicle = await _context.CustomerVehicles.AddAsync(new CustomerVehicle
                {
                    CompanyId = loggedInuser.CompanyId,
                    CustomerId = customerId,
                    StatusId = (int)VehicleStatusEnum.Checkin,
                    LicensePlate = model.CustomerVehicle.LicensePlate,
                    ChassisVin = model.CustomerVehicle.ChassinVin,
                    VehicleId = model.CustomerVehicle.VehicleId,
                    AverageVehicleValuation = model.CustomerVehicle.AverageVehicleValuation,
                    NextService = model.CustomerVehicle.NextServiceDate.Value,
                    RegistrationExpiryDate = model.CustomerVehicle.RegistrationExpiryDate.Value,
                    VehicleRegistrationFile = model.CustomerVehicle.RegistrationFile,
                    ServiceAdvisorId = model.CustomerVehicle.ServiceAdvisorId,
                    StartDate = model.CustomerContract.StartDate.Value,
                    EndDate = model.CustomerContract.EndDate.Value,
                    MODIFIED_BY = loggedInuser.UserId
                });

                await _context.SaveChangesAsync();
                customerVehicleId = customerVehicle.Entity.Id;
            }
            else
                customerVehicleId = model.CustomerVehicleId.Value;


            await _context.CustomerStorageContracts.AddAsync(
                new CustomerStorageContract()
                {
                    CompanyId = loggedInuser.CompanyId,
                    CustomerVehicleId = customerVehicleId,
                    CustomerId = customerId,
                    StartDate = model.CustomerContract.StartDate.Value,
                    EndDate = model.CustomerContract.EndDate.Value,
                    PricePerMonth = model.CustomerContract.PricePerMonth,
                    IsChargerSupplied = model.CustomerContract.IsChargerSupplied,
                    PaymentOptionId = model.CustomerContract.PaymentOptionId,
                    StorageBayId = model.CustomerContract.StorageBayId,
                    MODIFIED_BY = loggedInuser.UserId
                });


            var storageBay =
                await _context.StorageBays.FirstOrDefaultAsync(x =>
                    x.Id == model.CustomerContract.StorageBayId && x.CompanyId == loggedInuser.CompanyId);

            if (storageBay is not null)
                storageBay.StorageBayStatusId = (int)StorageBayStatusEnum.Unavailable;

            return true;
        });
    }

    public async Task<bool> UpdateCustomerDetails(UpdateCustomerDetails model)
    {
        return await UnitOfWork(async () =>
        {
            var loggedInuser = GetLoggedInUser();

            var customer = await _context.Customers.FirstOrDefaultAsync(x => x.Id == model.Customer.Id);

            if (customer is null)
                throw new ApiException("Could not find customer to edit");


            var user = await _userManager.FindByIdAsync(customer.UserId);

            if (user is null)
                throw new ApiException("Could not find user to edit");

            user.Email = model.Customer.Email;
            user.FullName = model.Customer.FullName;

            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                throw new ApiException("failed to update user details");

            customer.IdentificationNumber = model.Customer.IdentificationNumber;
            customer.IdentificationFile = model.Customer.IdentificationFile;
            customer.Address = model.Customer.Address;
            customer.ContactNumber = model.Customer.ContactNumber;


            // Step 1: Fetch existing records from the database
            var existingRecords = await _context.AdditionalContactPeople.Where(x => x.CustomerId == model.Customer.Id)
                .ToListAsync();

            // Step 2: Identify records to delete
            var idsInUpdate = model.AdditionalContactPersons.Select(x => x.Id).ToList();
            var recordsToDelete = existingRecords.Where(x => !idsInUpdate.Contains(x.Id)).ToList();

            // Remove records no longer in the updated model
            _context.AdditionalContactPeople.RemoveRange(recordsToDelete);

            // Step 3: Handle updates and additions
            foreach (var updatedItem in model.AdditionalContactPersons)
            {
                if (updatedItem.Id == 0)
                {
                    // Add new records
                    var newRecord = new AdditionalContactPerson
                    {
                        FullName = updatedItem.FullName,
                        IdentificationNumber = updatedItem.IdentificationNumber,
                        ContactNumber = updatedItem.ContactNumber,
                        Relationship = updatedItem.Relationship,
                        MODIFIED_BY = loggedInuser.UserId,
                        CustomerId = customer.Id,
                        CompanyId = customer.CompanyId,
                    };
                    _context.AdditionalContactPeople.Add(newRecord);
                }
                else
                {
                    // Update existing records
                    var existingRecord = existingRecords.FirstOrDefault(x => x.Id == updatedItem.Id);
                    if (existingRecord == null)
                        throw new ApiException("failed to additional contact person details to update");

                    existingRecord.FullName = updatedItem.FullName;
                    existingRecord.IdentificationNumber = updatedItem.IdentificationNumber;
                    existingRecord.ContactNumber = updatedItem.ContactNumber;
                    existingRecord.Relationship = updatedItem.Relationship;
                    existingRecord.MODIFIED_BY = loggedInuser.UserId;
                }
            }


            return true;
        });
    }
}