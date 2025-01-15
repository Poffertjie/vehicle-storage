using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.CustomerVehicle;
using Shared.Models.Media;

namespace AdminPortalAPI.Controllers;

public class MediaController : BaseController
{
    private readonly MediaService _mediaService;

    public MediaController(MediaService mediaService)
    {
        _mediaService = mediaService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetMedia([FromQuery] string id)
    {
        return Ok(await _mediaService.GetMedia(id));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateMedia([FromBody] CreateMediaRequestModel model)
    {
        return Ok(await _mediaService.CreateMedia(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateMedia([FromBody] UpdateMediaRequestModel model)
    {
        return Ok(await _mediaService.UpdateMedia(model));
    }
    
    [HttpGet]
    public async Task<IActionResult> DeleteMedia([FromQuery] string id)
    {
        return Ok(await _mediaService.DeleteMedia(id));
    }

}