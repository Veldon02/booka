using Booka.Infrastructure;
using Booka.WebApp.Mappers;
using Booka.WebApp.Extension;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddConfigurations(config);
builder.Services.ConfigureOptions();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllersWithOptions();
builder.Services.AddAuthentication(config);

builder.Services.AddExceptionHandling();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.MapControllers();

app.Run();
