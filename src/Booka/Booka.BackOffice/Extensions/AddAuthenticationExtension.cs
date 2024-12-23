using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Booka.BackOffice.Middleware;

namespace Booka.BackOffice.Extensions;

public static class AddAuthenticationExtension
{
    public static void AddAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtConfig:SecretKey"])),
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ValidateAudience = false,
            ValidateIssuer = false,
        };

        services.AddScoped<CustomJwtBearerEvents>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = tokenValidationParameters;
                options.EventsType = typeof(CustomJwtBearerEvents);
            });
    }
}