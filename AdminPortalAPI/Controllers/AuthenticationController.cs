using AdminPortalAPI.Attributes;
using AdminPortalAPI.Services;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using Shared.Helpers;
using Shared.Models.Auth;

namespace AdminPortalAPI.Controllers;

public class AuthenticationController : BaseController
{
    private readonly AuthenticationService _authenticationService;


    // TODO: get media / view document / view photo
    //todo: change service advisors to service garage - sub service advisors per service garage
    //todo: edit daily, weekly checks, check in, storage check in, check out
    //todo: all vehicle in storage and their status

    //todo: validation
    
    //reports
    // TODO: service and payments due
    // TODO: storage bays opening
    
    //questions
    //todo: change vehicle ownership - search existing or create new - if change ownership is done, is a new contract created for the new customer or do they take over the existing contract?
    
    //not priority
    //todo: creation of new contract add multiple vehicles
    //todo: apply roles to api
    
    
    
    public AuthenticationController(AuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost, AllowAnonymous, ValidateModel]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
    {
        var token = await _authenticationService.Login(model);
        return Ok(new LoginResponseModel
        {
            Token = token,
        });
    }

    [HttpPost, ValidateModel]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel model)
    {
        return Ok(await _authenticationService.CreateUser(model));
    }
    
    [HttpPost, ValidateModel]
    public async Task<IActionResult> UpdateUserRoles([FromBody] UpdateUserRolesRequestModel model)
    {
        return Ok(await _authenticationService.UpdateUserRoles(model));
    }
    
    [HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetUsersAndRoles()
    {
        return Ok(await _authenticationService.GetUsersAndRoles());
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        return Ok(await _authenticationService.GetRoles());
    }
}