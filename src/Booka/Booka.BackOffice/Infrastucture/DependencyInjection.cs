using Booka.Application.Services;
using Booka.BackOffice.Middleware;
using Booka.Domain.Interfaces.Repositories;
using Booka.Domain.Interfaces.Services;
using Booka.Infrastructure.Database.Repositories;

namespace Booka.BackOffice.Infrastucture;

public static class DependencyInjection
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddServices();
        services.AddRepositories();
        services.AddExceptionHandling();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceService, WorkspaceService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
    }

    private static void AddExceptionHandling(this IServiceCollection services)
    {
        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();
    }
}