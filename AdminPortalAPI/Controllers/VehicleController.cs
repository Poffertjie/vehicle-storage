using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.Vehicles;

namespace AdminPortalAPI.Controllers;

public class VehicleController : BaseController
{
    private readonly VehicleService _vehicleService;

    public VehicleController(VehicleService vehicleService)
    {
        _vehicleService = vehicleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetVehicles()
    {
        return Ok(await _vehicleService.GetVehicles());
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetVehicleId([FromQuery] int brandId, int brandModelId, int? brandModelVariantId)
    {
        return Ok(await _vehicleService.GetVehicleId(brandId, brandModelId, brandModelVariantId));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateVehicle([FromBody] CreateVehicleRequestModel model)
    {
        return Ok(await _vehicleService.CreateVehicle(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateVehicle([FromBody] UpdateVehicleRequestModel model)
    {
        return Ok(await _vehicleService.UpdateVehicle(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        return Ok(await _vehicleService.GetBrands());
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateBrand([FromBody] CreateBrandRequestModel model)
    {
        return Ok(await _vehicleService.CreateBrand(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateBrand([FromBody] UpdateBrandRequestModel model)
    {
        return Ok(await _vehicleService.UpdateBrand(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandModels([FromQuery] int brandId)
    {
        return Ok(await _vehicleService.GetBrandModels(brandId));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateBrandModel([FromBody] CreateBrandModelRequestModel model)
    {
        return Ok(await _vehicleService.CreateBrandModel(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateBrandModel([FromBody] UpdateBrandModelRequestModel model)
    {
        return Ok(await _vehicleService.UpdateBrandModel(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetBrandModelVariants([FromQuery] int brandModelId)
    {
        return Ok(await _vehicleService.GetBrandModelVariants(brandModelId));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateBrandModelVariant([FromBody] CreateBrandModelVariantRequestModel model)
    {
        return Ok(await _vehicleService.CreateBrandModelVariant(model));
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateBrandModelVariant([FromBody] UpdateBrandModelVariantRequestModel model)
    {
        return Ok(await _vehicleService.UpdateBrandModelVariant(model));
    }
}