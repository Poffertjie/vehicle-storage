using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Auth;
using Shared.Models.Customer;

namespace AdminPortalAPI.Controllers;

public class CustomerController : BaseController
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomers()
    {
        return Ok(await _customerService.GetCustomers());
    }

    [HttpGet]
    public async Task<IActionResult> GetCustomer([FromQuery] int customerId)
    {
        return Ok(await _customerService.GetCustomer(customerId));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateCustomerContract([FromBody] CreateCustomerStorageContract model)
    {
        return Ok(await _customerService.CreateCustomerContract(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateCustomerDetails([FromBody] UpdateCustomerDetails model)
    {
        return Ok(await _customerService.UpdateCustomerDetails(model));
    }
}