using Pacogroup.Ecommerce.Application.Main;
using Pacogroup.Ecommerce.Domain.Core;
using Pacogroup.Ecommerce.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Inyeccion de los metodos de extension de cadfa una de las capas de la solucion
builder.Services.AddDomainServices();
builder.Services.AddInfraestrutureServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
