using Booka.Application.Services;
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
       
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceService, WorkspaceService>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
    }
}