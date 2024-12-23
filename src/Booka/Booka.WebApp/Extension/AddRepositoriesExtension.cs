using Booka.Core.Interfaces.Repositories;
using Booka.Infrastructure.Database.Repositories;

namespace Booka.WebApp.Extension;

public static class AddRepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
    }
}