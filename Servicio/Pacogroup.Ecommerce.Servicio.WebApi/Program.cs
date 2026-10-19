using Pacogroup.Ecommerce.Application.Main;
using Pacogroup.Ecommerce.Domain.Core;
using Pacogroup.Ecommerce.Infrastructure.Repository;
using Pacogroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacogroup.Ecommerce.Services.WebApi.Modules.Validator;
using Pacogroup.Ecommerce.Services.WebApi.Modules.Versioning;
using Pacogroup.Ecommerce.Transversal.Logging;
using Serilog;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Pacogroup.Ecommerce.Services.WebApi.Modules.HealthChecks;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Nombre de la politica y extraccion de los origenes permitidos desde el archivo de configuracion
var mypolicy = "policyApiEcommerce";
var alllowedOrigins = builder.Configuration.GetSection("Cors:OriginCors").Get<string[]>() ?? [];

// Add services to the container.
builder.Services.AddControllers();

// Activacion de la politica de origenes cruzados CORS
builder.Services.AddCors(options =>
    options.AddPolicy(
        mypolicy, policy =>
            policy.WithOrigins(alllowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod()
    )
);

// Inyeccion de los metodos de extension de cadfa una de las capas de la solucion
builder.Services.AddDomainServices();
builder.Services.AddInfraestrutureServices();
builder.Services.AddApplicationServices();
builder.Services.AddAuth(builder.Configuration); // Adicion de la autenticacion con jwt
builder.Services.AddTransversalServices(builder.Configuration); // Inyeccion de la extension para logs
builder.Host.UseSerilog();
builder.Services.AddValidator();
builder.Services.AddHealthCheck(builder.Configuration);

// Inyeccion de Swagger y versioning
builder.Services.AddVersioning();
builder.Services.AddApiDocumentation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseApiDocumentation();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();

app.UseCors(mypolicy); // Habilitamos el uso de cors con la politica definda

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHealthChecksUI(); // mapeo del dashboard desde el cual se veran los estados de salud
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse

});

try
{
    Log.Information("Starting Pacagroup.Ecommerce API");
    app.Run();
}
catch (System.Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { };