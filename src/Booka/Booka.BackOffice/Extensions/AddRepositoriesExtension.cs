using Booka.Core.Interfaces.Repositories;
using Booka.Infrastructure.Database.Repositories;

namespace Booka.BackOffice.Extensions;

public static class AddRepositoriesExtension
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceRepository, WorkspaceRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}