using Booka.Core.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Booka.WebApp.Filters;

public class OnlyCurrentUserFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var stringId = context.RouteData.Values["userId"]?.ToString();

        if (!int.TryParse(stringId, out var requestedUserId) || requestedUserId <= 0)
        {
            throw new InvalidParametersException($"Invalid user id {requestedUserId}");
        }

        var userIdClaim = context.HttpContext.User.Claims.First(x => x.Type is JwtRegisteredClaimNames.Sub);

        var loggedUserId = int.Parse(userIdClaim.Value);

        if (requestedUserId != loggedUserId)
        {
            throw new ForbiddenException("You are not allowed to access this user");
        }

        await next();
    }
}