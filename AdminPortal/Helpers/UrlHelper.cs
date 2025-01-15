namespace AdminPortal.Helpers;

public class UrlHelper
{
    public Authentication Authentication => new("Authentication");
    public Customer Customer => new("Customer");
    public CustomerVehicle CustomerVehicle => new("CustomerVehicle");
    public Media Media => new("Media");
    public ServiceAdvisor ServiceAdvisor => new("ServiceAdvisor");
    public Vehicle Vehicle => new("Vehicle");
    public Lookup Lookup => new("Lookup");
    public Storage Storage => new("Storage");
    public Company Company => new("Company");
}

public class Authentication(string url) : BaseURL(url)
{
    public string Login => CreateURL("Login");
    public string CreateUser => CreateURL("CreateUser");
    public string GetRoles => CreateURL("GetRoles");
    public string GetUsersAndRoles => CreateURL("GetUsersAndRoles");
    public string UpdateUserRoles => CreateURL("UpdateUserRoles");
}

public class Customer(string url) : BaseURL(url)
{
    public string GetCustomers => CreateURL("GetCustomers");
    public string GetCustomer(int customerId) => CreateURL($"GetCustomer?customerId={customerId}");
    public string CreateCustomerContract => CreateURL("CreateCustomerContract");
    public string UpdateCustomerDetails => CreateURL("UpdateCustomerDetails");
}

public class CustomerVehicle(string url) : BaseURL(url)
{
    public string GetVehiclesForCheckIn => CreateURL("GetVehiclesForCheckIn");
    public string GetVehiclesForCheckOut => CreateURL("GetVehiclesForCheckOut");
    public string GetVehiclesCheckOut => CreateURL("GetVehiclesCheckOut");
    public string GetVehiclesForStorageCheckIn => CreateURL("GetVehiclesForStorageCheckIn");
    public string GetVehiclesInStorage => CreateURL("GetVehiclesInStorage");
    public string CustomerVehicleCheckIn => CreateURL("CustomerVehicleCheckIn");
    public string StartCustomerVehicleCheckOut => CreateURL("StartCustomerVehicleCheckOut");
    public string EndCustomerVehicleCheckOut => CreateURL("EndCustomerVehicleCheckOut");
    public string CustomerVehicleStorageCheckIn => CreateURL("CustomerVehicleStorageCheckIn");
    public string StartCustomerVehicleCheckInAfterCheckedOutState(int customerVehicleId,  int customerStorageContractId) => CreateURL($"StartCustomerVehicleCheckInAfterCheckedOutState?customerVehicleId={customerVehicleId}&customerStorageContractId={customerStorageContractId}");
    public string GetDailyChecks => CreateURL("GetDailyChecks");
    public string GetDailyCheck(int id) => CreateURL($"GetDailyCheck?id={id}");
    public string CustomerVehicleDailyCheck => CreateURL("CustomerVehicleDailyCheck");
    public string GetWeeklyChecks => CreateURL("GetWeeklyChecks");
    public string GetWeeklyCheck(int id) => CreateURL($"GetWeeklyCheck?id={id}");
    public string CustomerVehicleWeeklyCheck => CreateURL("CustomerVehicleWeeklyCheck");
    public string GetCustomerVehicleForUpdate(int customerVehicleId) => CreateURL($"GetCustomerVehicleForUpdate?customerVehicleId={customerVehicleId}");
    public string UpdateCustomerVehicle => CreateURL("UpdateCustomerVehicle");
    public string GetCustomerContractForUpdate(int customerContractId) => CreateURL($"GetCustomerContractForUpdate?customerContractId={customerContractId}");
    public string UpdateCustomerContract => CreateURL("UpdateCustomerContract");
   
}

public class Media(string url) : BaseURL(url)
{
    public string GetMedia(string id) => CreateURL($"GetMedia?id={id}");
    public string CreateMedia => CreateURL("CreateMedia");
    public string UpdateMedia => CreateURL("UpdateMedia");
    public string DeleteMedia(string id) => CreateURL($"DeleteMedia?id={id}");
}

public class ServiceAdvisor(string url) : BaseURL(url)
{
    public string GetServiceAdvisors => CreateURL("GetServiceAdvisors");
    public string CreateServiceAdvisor => CreateURL("CreateServiceAdvisor");
    public string UpdateServiceAdvisor => CreateURL("UpdateServiceAdvisor");
}

public class Vehicle(string url) : BaseURL(url)
{
    public string GetVehicleId(int brandId, int brandModelId, int? brandModelVariantId) => CreateURL($"GetVehicleId?brandId={brandId}&brandModelId={brandModelId}&brandModelVariantId={brandModelVariantId}");
    public string GetVehicles => CreateURL("GetVehicles");
    public string CreateVehicle => CreateURL("CreateVehicle");
    public string UpdateVehicle => CreateURL("UpdateVehicle");
    public string GetBrands => CreateURL("GetBrands");
    public string CreateBrand => CreateURL("CreateBrand");
    public string UpdateBrand => CreateURL("UpdateBrand");
    public string GetBrandModels(int id) => CreateURL($"GetBrandModels?brandId={id}");
    public string CreateBrandModel => CreateURL("CreateBrandModel");
    public string UpdateBrandModel => CreateURL("UpdateBrandModel");
    public string GetBrandModelVariants(int id) => CreateURL($"GetBrandModelVariants?brandModelId={id}");
    public string CreateBrandModelVariant => CreateURL("CreateBrandModelVariant");
    public string UpdateBrandModelVariant => CreateURL("UpdateBrandModelVariant");
}

public class Lookup(string url) : BaseURL(url)
{
    public string GetPaymentOptions => CreateURL("GetPaymentOptions");
    public string GetDeliveryMethods => CreateURL("GetDeliveryMethods");
}

public class Storage(string url) : BaseURL(url)
{
    public string GetAvailableStorageBays => CreateURL("GetAvailableStorageBays");
    public string GetStorageAllBays => CreateURL("GetStorageAllBays");
    public string CreateStorageBay => CreateURL("CreateStorageBay");
    public string UpdateStorageBay => CreateURL("UpdateStorageBay");
    public string StorageBayNumberExist(int number) => CreateURL($"StorageBayNumberExist?number={number}");
}

public class Company(string url) : BaseURL(url)
{
    public string GetCompany => CreateURL("GetCompany");
    public string UpdateCompany => CreateURL("UpdateCompany");
}



public abstract class BaseURL
{
    private readonly string _controller;
    protected BaseURL(string controller)
    {
        _controller = controller;
    }

    protected string CreateURL(string url)
    {
        return $"{_controller}/{url}";
    }
}