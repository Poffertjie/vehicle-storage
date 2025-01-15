using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Company;

namespace AdminPortalAPI.Controllers;

public class CompanyController : BaseController
{
    private readonly CompanyService _companyService;

    public CompanyController(CompanyService companyService)
    {
        _companyService = companyService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetCompany()
    {
        return Ok(await _companyService.GetCompany());
    }
    
    [HttpPost]
    public async Task<IActionResult> UpdateCompany([FromBody] UpdateCompanyResponseModel model)
    {
        return Ok(await _companyService.UpdateCompany(model));
    }
}