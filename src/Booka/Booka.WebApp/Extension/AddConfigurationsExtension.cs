using Booka.Infrastructure.Configuration;

namespace Booka.WebApp.Extension;

public static class AddConfigurationsExtension
{
    public static void AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtConfig>(configuration.GetSection("JwtConfig"));
    }
}
