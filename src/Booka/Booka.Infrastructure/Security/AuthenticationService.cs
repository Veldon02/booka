using Booka.Core.Domain;
using Booka.Core.DTOs.Security;
using Booka.Core.Exceptions;
using Booka.Core.Interfaces.Repositories;
using Booka.Core.Interfaces.Security;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Booka.Infrastructure.Security;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly IWorkspaceRepository _workspaceRepository;
    private readonly IJwtService _jwtService;
    private readonly IHasher _hasher;

    public AuthenticationService(IUserRepository userRepository, IJwtService jwtService, IHasher hasher, IWorkspaceRepository workspaceRepository)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _hasher = hasher;
        _workspaceRepository = workspaceRepository;
    }

    public async Task<UserAuthenticationResult> GetUserTokenAsync(TokenRequestDto tokenRequestDto)
    {
        var user = await _userRepository.GetByEmail(tokenRequestDto.Email);

        var verified = _hasher.Verify(user?.Password, tokenRequestDto.Password);

        if (user is null || !verified)
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