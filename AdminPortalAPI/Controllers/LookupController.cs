using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdminPortalAPI.Controllers;

public class LookupController : BaseController
{
    private readonly LookupService _service;

    public LookupController(LookupService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPaymentOptions()
    {
        return Ok(await _service.GetPaymentOptions());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetDeliveryMethods()
    {
        return Ok(await _service.GetDeliveryMethods());
    }
}