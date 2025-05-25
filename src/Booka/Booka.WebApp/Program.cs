using Booka.Infrastructure;
using Booka.WebApp.Mappers;
using Booka.WebApp.Extension;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddServices(config);
builder.Services.AddRepositories();
builder.Services.AddConfigurations(config);
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllersWithOptions();
builder.Services.AddAuthentication(config);

builder.Services.AddExceptionHandling();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger(options =>
{
    options.RouteTemplate = "/openapi/{documentName}.json";
});
app.MapScalarApiReference();

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
