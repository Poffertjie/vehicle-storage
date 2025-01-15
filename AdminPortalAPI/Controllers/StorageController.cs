using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Models.Storage;

namespace AdminPortalAPI.Controllers;

public class StorageController : BaseController
{
    private readonly StorageService _storageService;

    public StorageController(StorageService storageService)
    {
        _storageService = storageService;
    }
    
        
    [HttpGet]
    public async Task<IActionResult> GetStorageAllBays()
    {
        return Ok(await _storageService.GetStorageAllBays());
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAvailableStorageBays()
    {
        return Ok(await _storageService.GetStorageBays(StorageBayStatusEnum.Available));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateStorageBay([FromBody] CreateStorageBayRequestModel model)
    {
        return Ok(await _storageService.CreateStorageBay(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateStorageBay([FromBody] UpdateStorageBayRequestModel model)
    {
        return Ok(await _storageService.UpdateStorageBay(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> StorageBayNumberExist([FromQuery] int number)
    {
        return Ok(await _storageService.StorageBayNumberExist(number));
    }
}