using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Customer;
using Shared.Models.CustomerVehicle;

namespace AdminPortalAPI.Controllers;

public class CustomerVehicleController : BaseController
{
    private readonly CustomerVehicleService _customerVehicleService;

    public CustomerVehicleController(CustomerVehicleService customerVehicleService)
    {
        _customerVehicleService = customerVehicleService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVehiclesForCheckIn()
    {
        return Ok(await _customerVehicleService.GetVehiclesForCheckIn());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVehiclesForCheckOut()
    {
        return Ok(await _customerVehicleService.GetVehiclesForCheckOut());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVehiclesCheckOut()
    {
        return Ok(await _customerVehicleService.GetVehiclesCheckOut());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVehiclesForStorageCheckIn()
    {
        return Ok(await _customerVehicleService.GetVehiclesForStorageCheckIn());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetVehiclesInStorage()
    {
        return Ok(await _customerVehicleService.GetVehiclesInStorage());
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CustomerVehicleCheckIn([FromBody] CustomerVehicleCheckInRequestModel model)
    {
        return Ok(await _customerVehicleService.CustomerVehicleCheckIn(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> StartCustomerVehicleCheckOut([FromBody] StartCustomerVehicleCheckOutRequestModel model)
    {
        return Ok(await _customerVehicleService.StartCustomerVehicleCheckOut(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> EndCustomerVehicleCheckOut([FromBody] EndCustomerVehicleCheckOutRequestModel model)
    {
        return Ok(await _customerVehicleService.EndCustomerVehicleCheckOut(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CustomerVehicleStorageCheckIn([FromBody] CustomerVehicleStorageCheckInRequestModel model)
    {
        return Ok(await _customerVehicleService.CustomerVehicleStorageCheckIn(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> StartCustomerVehicleCheckInAfterCheckedOutState([FromQuery] int customerVehicleId,  int customerStorageContractId)
    {
        return Ok(await _customerVehicleService.StartCustomerVehicleCheckInAfterCheckedOutState(customerVehicleId, customerStorageContractId));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDailyChecks()
    {
        return Ok(await _customerVehicleService.GetDailyChecks());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDailyCheck([FromQuery] int id)
    {
        return Ok(await _customerVehicleService.GetDailyCheck(id));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CustomerVehicleDailyCheck([FromBody] CustomerVehicleDailyCheckRequestModel model)
    {
        return Ok(await _customerVehicleService.CustomerVehicleDailyCheck(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetWeeklyChecks()
    {
        return Ok(await _customerVehicleService.GetWeeklyChecks());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetWeeklyCheck([FromQuery] int id)
    {
        return Ok(await _customerVehicleService.GetWeeklyCheck(id));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CustomerVehicleWeeklyCheck([FromBody] CustomerVehicleWeeklyCheckRequestModel model)
    {
        return Ok(await _customerVehicleService.CustomerVehicleWeeklyCheck(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomerVehicleForUpdate([FromQuery] int customerVehicleId)
    {
        return Ok(await _customerVehicleService.GetCustomerVehicleForUpdate(customerVehicleId));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateCustomerVehicle([FromBody] UpdateCustomerVehicle model)
    {
        return Ok(await _customerVehicleService.UpdateCustomerVehicle(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCustomerContractForUpdate([FromQuery] int customerContractId)
    {
        return Ok(await _customerVehicleService.GetCustomerContractForUpdate(customerContractId));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateCustomerContract([FromBody] UpdateCustomerContract model)
    {
        return Ok(await _customerVehicleService.UpdateCustomerContract(model));
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> CreateDailyCheckEntries()
    {
        await _customerVehicleService.CreateDailyCheckEntries();
        return Ok();
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> CreateWeeklyCheckEntries()
    {
        await _customerVehicleService.CreateWeeklyCheckEntries();
        return Ok();
    }
}