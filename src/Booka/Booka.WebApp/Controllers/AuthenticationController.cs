using AutoMapper;
using Booka.Core.Domain;
using Booka.Core.DTOs;
using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Booka.WebApp.ApiModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booka.WebApp.Controllers;

[Route("api/auth")]
public class AuthenticationController : BaseController
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper, IUserService userService)
    {
        _authenticationService = authenticationService;
        _mapper = mapper;
        _userService = userService;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserAuthenticationResult>> Register(UserRegisterRequest request)
    {
        var result = _authenticationService.GetUserToken(await _userService.AddAsync(_mapper.Map<User>(request)));

        return Ok(result);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserAuthenticationResult>> LogIn(UserLogInRequest request)
    {
        var result = await _authenticationService.GetUserTokenAsync(_mapper.Map<TokenRequestDto>(request));

        return Ok(result);
    }
}