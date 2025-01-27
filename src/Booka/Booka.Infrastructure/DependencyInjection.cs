using Booka.Core.Interfaces.Security;
using Booka.Infrastructure.Database;
using Booka.Infrastructure.Security;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Booka.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<BookaDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("Database"))
                   .UseExceptionProcessor();
        });

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IHasher, Hasher>();
    }
}
