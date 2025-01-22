using AutoMapper;
using Booka.BackOffice.ApiModels.Authentication;
using Booka.Core.Domain;
using Booka.Core.DTOs.Security;
using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Controller;

[Route("auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IWorkspaceService _workspaceService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IWorkspaceService workspaceService)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _workspaceService = workspaceService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<WorkspaceAuthenticationResult>> Register(RegisterWorkspaceRequest request)
    {
        var result = _authenticationService.GetWorkspaceToken(
            await _workspaceService.AddAsync(_mapper.Map<Workspace>(request)));

        return Ok(result);
    }


    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<WorkspaceAuthenticationResult>> Login(LoginWorkspaceRequest request)
    {
        var result = await _authenticationService.GetWorkspaceTokenAsync(_mapper.Map<TokenRequestDto>(request));

        return Ok(result);
    }
}