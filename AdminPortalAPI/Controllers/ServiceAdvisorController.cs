using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.ServiceAdvisor;
using Shared.Models.Vehicles;

namespace AdminPortalAPI.Controllers;

public class ServiceAdvisorController : BaseController
{
    private readonly ServiceAdvisorService _serviceAdvisorService;

    public ServiceAdvisorController(ServiceAdvisorService serviceAdvisorService)
    {
        _serviceAdvisorService = serviceAdvisorService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetServiceAdvisors()
    {
        return Ok(await _serviceAdvisorService.GetServiceAdvisors());
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateServiceAdvisor([FromBody] CreateServiceAdvisorRequestModel model)
    {
        return Ok(await _serviceAdvisorService.CreateServiceAdvisor(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateServiceAdvisor([FromBody] UpdateServiceAdvisorRequestModel model)
    {
        return Ok(await _serviceAdvisorService.UpdateServiceAdvisor(model));
    }
}