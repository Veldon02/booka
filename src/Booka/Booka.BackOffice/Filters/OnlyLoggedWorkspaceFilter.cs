using System.IdentityModel.Tokens.Jwt;
using Booka.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Booka.BackOffice.Filters;

public class OnlyLoggedWorkspaceFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stringId = context.RouteData.Values["workspaceId"]?.ToString();

        if (!int.TryParse(stringId, out var requestedWorkspaceId) || requestedWorkspaceId <= 0)
        {
            throw new InvalidParametersException("Invalid workspace id");
        }

        var workspaceClaim = context.HttpContext.User.Claims.First(x => x.Type is JwtRegisteredClaimNames.Sub);

        var loggedWorkspaceId = int.Parse(workspaceClaim.Value);

        if (requestedWorkspaceId != loggedWorkspaceId)
        {
            throw new ForbiddenException("You are not allowed to access this workspace");
        }

        await next();
    }
}