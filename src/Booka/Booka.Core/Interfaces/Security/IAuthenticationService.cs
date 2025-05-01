using System.Security.Claims;
using Booka.Core.Domain;
using Booka.Core.Domain.enums.Auth;
using Booka.Core.DTOs.Security;

namespace Booka.Core.Interfaces.Security;

public interface IAuthenticationService
{
    Task<UserAuthenticationResult> GetUserTokenAsync(TokenRequestDto tokenRequestDto);

    UserAuthenticationResult GetUserToken(User user);

    Task<UserAuthenticationResult> HandleGoogleUser(ClaimsPrincipal principal, AuthType authType);

    Task<WorkspaceAuthenticationResult> GetWorkspaceTokenAsync(TokenRequestDto tokenRequestDto);

    WorkspaceAuthenticationResult GetWorkspaceToken(Workspace workspace);
}