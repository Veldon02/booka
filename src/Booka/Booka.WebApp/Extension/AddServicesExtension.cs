using Booka.Core.Interfaces.Azure;
using Booka.Core.Interfaces.Security;
using Booka.Core.Interfaces.Services;
using Booka.Core.Services;
using Booka.Infrastructure.Azure;
using Booka.Infrastructure.QrCodes;
using Booka.Infrastructure.Security;
using Microsoft.Extensions.Azure;

namespace Booka.WebApp.Extension;

public static class AddServicesExtension
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IWorkspaceService, WorkspaceService>();
        services.AddScoped<IWorkplaceService, WorkplaceService>();
        services.AddScoped<IBookingService, BookingService>();
        services.AddScoped<IQrCodeGeneratorService, QrCodeGeneratorService>();

        services.AddAzureClients(client =>
        {
            client.AddServiceBusClient(configuration["ServiceBus:ConnectionString"]);
        });

        services.AddSingleton<IEmailNotificationService, EmailNotificationService>();
    }
}