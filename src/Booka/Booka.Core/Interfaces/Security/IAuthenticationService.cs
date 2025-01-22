using Booka.Core.Domain;
using Booka.Core.DTOs.Security;

namespace Booka.Core.Interfaces.Security;

public interface IAuthenticationService
{
    Task<UserAuthenticationResult> GetUserTokenAsync(TokenRequestDto tokenRequestDto);

    UserAuthenticationResult GetUserToken(User user);

    Task<WorkspaceAuthenticationResult> GetWorkspaceTokenAsync(TokenRequestDto tokenRequestDto);

    WorkspaceAuthenticationResult GetWorkspaceToken(Workspace workspace);
}