using Booka.WebApp.Middleware;

namespace Booka.WebApp.Extension;

public static class AddExceptionHandlingExtenstion
{
    public static void AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
    }
}