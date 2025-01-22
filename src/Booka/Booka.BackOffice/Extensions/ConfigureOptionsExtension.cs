using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Booka.BackOffice.Extensions;

public static class ConfigureOptionsExtension
{
    public static void ConfigureOptions(this IServiceCollection services)
    {
        services.Configure<JsonOptions>(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}