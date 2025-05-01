using System.Security.Claims;
using AutoMapper;
using Booka.Core.Domain;
using Booka.Core.Domain.enums.Auth;
using Booka.Core.DTOs.Security;
using Booka.Core.Interfaces.Services;
using Booka.WebApp.ApiModels.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IAuthenticationService = Booka.Core.Interfaces.Security.IAuthenticationService;

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

    [HttpGet("login/google")]
    [AllowAnonymous]
    public ActionResult LogInGoogle()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = $"{Url.Action(nameof(GoogleAuth))}?authType={nameof(AuthType.Login)}",
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("register/google")]
    [AllowAnonymous]
    public ActionResult RegisterGoogle()
    {
        var properties = new AuthenticationProperties
        {
            RedirectUri = $"{Url.Action(nameof(GoogleAuth))}?authType={nameof(AuthType.Register)}",
        };

        return Challenge(properties, GoogleDefaults.AuthenticationScheme);
    }

    [HttpGet("google")]
    [AllowAnonymous]
    public async Task<ActionResult<string>> GoogleAuth([FromQuery] AuthType authType)
    {
        var authentication = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

        if (!authentication.Succeeded)
        {
            return Unauthorized();
        }

        var result = await _authenticationService.HandleGoogleUser(authentication.Principal, authType);

        return Ok(result);
    } 
}