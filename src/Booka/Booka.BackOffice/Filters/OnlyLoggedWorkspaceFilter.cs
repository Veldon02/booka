using System.IdentityModel.Tokens.Jwt;
using Booka.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booka.BackOffice.Filters;

public class OnlyLoggedWorkspaceFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stringId = context.RouteData.Values["workspaceId"]?.ToString();

        if (!int.TryParse(stringId, out var requestedWorkspaceId))
        {
            throw new InvalidParametersException("Invalid workspace");
        }

        var workspaceClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type is JwtRegisteredClaimNames.Sub);

        if (workspaceClaim == null)
        {
            throw new InvalidTokenException("Invalid token");
        }

        if (!int.TryParse(workspaceClaim.Value, out var loggedWorkspaceId))
        {
            throw new InvalidParametersException("Invalid workspace");
        }

        if (requestedWorkspaceId != loggedWorkspaceId)
        {
            throw new ForbiddenException("You are not allowed to access this workspace");
        }

        await next();
    }
}