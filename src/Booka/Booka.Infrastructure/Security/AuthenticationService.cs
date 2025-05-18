using System.Security.Claims;
using Booka.Core.Domain;
using Booka.Core.Domain.enums.Auth;
using Booka.Core.DTOs.Security;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Booka.Infrastructure.Security;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IJwtService _jwtService;
    private readonly IHasher _hasher;

    public AuthenticationService(IUserService userService, IJwtService jwtService, IHasher hasher, IWorkspaceRepository workspaceRepository)
    {
        _userService = userService;
        _jwtService = jwtService;
        _hasher = hasher;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<UserAuthenticationResult> GetUserTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var user = await _userService.GetByEmailAsync(tokenRequestDto.Email);

        if (user?.Password is null)
        {
            throw new NotFoundException($"User {tokenRequestDto.Email} not found");
        }

        var verified = _hasher.Verify(user.Password, tokenRequestDto.Password);

        if (!verified)
        {
            throw new InvalidParametersException("email or password is invalid");
        }

        return GetUserToken(user);
    }

    public UserAuthenticationResult GetUserToken(User user)
    {
        var claims = new Dictionary<string, string>
        {
            [JwtRegisteredClaimNames.Sub] = user.Id.ToString(),
            [JwtRegisteredClaimNames.Email] = user.Email
        };

        var token = _jwtService.GenerateToken(claims);

        return new UserAuthenticationResult { Token = token, User = user.Id };
    }

    public async Task<UserAuthenticationResult> HandleGoogleUser(ClaimsPrincipal principal, AuthType authType)
    {
        return authType switch
        {
            AuthType.Login => await LoginGoogleUser(principal),
            AuthType.Register => await RegisterGoogleUser(principal),
            _ => throw new InvalidParametersException("Invalid authentication type")
        };
    }

    private async Task<UserAuthenticationResult> LoginGoogleUser(ClaimsPrincipal principal)
    {
        var email = principal.FindFirstValue(ClaimTypes.Email);
        var user = await _userService.GetByEmailAsync(email);

        if (user is null)
        {
            throw new NotFoundException($"User {email} do not exist");
        }

        return GetUserToken(user);
    }

    private async Task<UserAuthenticationResult> RegisterGoogleUser(ClaimsPrincipal principal)
    {
        var email = principal.FindFirstValue(ClaimTypes.Email)
            ?? throw new InvalidParametersException("Email is missing");

        var name = principal.FindFirstValue(ClaimTypes.Name)
                    ?? throw new InvalidParametersException("Name is missing");

        var user = await _userService.AddAsync(new User
        {
            Email = email,
            FirstName = name,
            LastName = principal.FindFirstValue(ClaimTypes.Surname) ?? string.Empty,
            Password = null,
        });

        return GetUserToken(user);
    }

    public async Task<WorkspaceAuthenticationResult> GetWorkspaceTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var workspace = await _workspaceRepository.GetByEmail(tokenRequestDto.Email);

        var verified = _hasher.Verify(workspace?.Password, tokenRequestDto.Password);

        if (workspace is null || !verified)
        {
            throw new InvalidParametersException("email or password is invalid");
        }

        return GetWorkspaceToken(workspace);
    }

    public WorkspaceAuthenticationResult GetWorkspaceToken(Workspace workspace)
    {
        var claims = new Dictionary<string, string>
        {
            [JwtRegisteredClaimNames.Sub] = workspace.Id.ToString(),
            [JwtRegisteredClaimNames.Email] = workspace.Email
        };

        var token = _jwtService.GenerateToken(claims);

        return new WorkspaceAuthenticationResult { Token = token, Workspace = workspace.Id };
    }
}