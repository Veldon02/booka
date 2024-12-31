using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ApplicationException = Booka.Core.Exceptions.ApplicationException;

namespace Booka.BackOffice.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Exception occurred: {Message}", exception.Message);

        var statusCode = exception is ApplicationException e ? e.StatusCode : HttpStatusCode.InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Status = (int)statusCode,
            Title = "An error occured",
            Detail = exception.Message,
            Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
            Extensions = new Dictionary<string, object?>
            {
                { "requestId", httpContext.TraceIdentifier },
            },
        };

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}