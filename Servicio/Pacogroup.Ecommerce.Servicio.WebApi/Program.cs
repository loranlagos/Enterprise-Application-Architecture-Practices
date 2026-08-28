using Pacogroup.Ecommerce.Application.Main;
using Pacogroup.Ecommerce.Domain.Core;
using Pacogroup.Ecommerce.Infrastructure.Repository;
using Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Nombre de la politica y extraccion de los origenes permitidos desde el archivo de configuracion
var mypolicy = "policyApiEcommerce";
var alllowedOrigins = builder.Configuration.GetSection("Cors:OriginCors").Get<string[]>() ?? [];


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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

// Inyeccion de Swagger
builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); // ruta del endpoint
        c.RoutePrefix = "swagger"; // prefijo de la ruta de swagger
        c.DisplayRequestDuration(); // permite visualizar la duracion de las peticiones
        c.EnableDeepLinking();
        c.ShowExtensions(); // permite ver campos y valores para operaciones y esquemas
    });
    //app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors(mypolicy); // Habilitamos el uso de cors con la politica definda

app.UseAuthorization();

app.MapControllers();

app.Run();
