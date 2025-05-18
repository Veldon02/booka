using Booka.EmailNotificationFunctionApp.Interfaces;
using Booka.EmailNotificationFunctionApp.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication()
    .Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();

await host.RunAsync();