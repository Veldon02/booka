using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Booka.Core.Services;
using Booka.Infrastructure.Security;

namespace Booka.BackOffice.Extensions;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IWorkspaceService, WorkspaceService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IWorkplaceService, WorkplaceService>();
    }
}