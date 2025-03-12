using Booka.Infrastructure.Configuration;

namespace Booka.BackOffice.Extensions;

public static class AddConfigurationsExtension
{
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
        services.Configure<QrConfig>(configuration.GetSection("QrConfig"));
    }
}
