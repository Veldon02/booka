using Booka.BackOffice.Middleware;

namespace Booka.BackOffice.Extensions;

public static class AddExceptionHandlingExtenstion
{
    public static void AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
    }
}