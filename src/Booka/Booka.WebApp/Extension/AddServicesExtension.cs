using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Booka.Core.Services;
using Booka.Infrastructure.Security;

namespace Booka.WebApp.Extension;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IWorkspaceService, WorkspaceService>();
    }
}