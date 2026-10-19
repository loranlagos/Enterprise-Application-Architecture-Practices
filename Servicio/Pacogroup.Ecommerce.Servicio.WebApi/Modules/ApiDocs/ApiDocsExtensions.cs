using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public static class ApiDocsExtensions
    {
        /// <summary>
        /// Genera un documento OpenAPI por version (Swashbuckle)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiDocumentation(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureApiDocsOptions>();
            services.AddSwaggerGen(c =>
            {
                // Cada documento incluye solo los endpoints de su version
                c.DocInclusionPredicate(
                    (documentName, apiDescription) =>
                        apiDescription.GroupName == documentName
                );

                // Inclusion del archivo xml generado automaticamente
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                // Documentacion con OpenApi para que swagger maneje la autenticacion
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Enter JWT Bearer token **_only_**",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",

                };

                c.AddSecurityDefinition(
                    JwtBearerDefaults.AuthenticationScheme,
                    securityScheme
                );

                c.AddSecurityRequirement(document => new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference(
                        JwtBearerDefaults.AuthenticationScheme,
                        document
                    )] = []
                });

                c.EnableAnnotations();
            });

            return services;
        }

        /// <summary>
        /// Publica el JSON de cada version y la UI de Scalar
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static WebApplication UseApiDocumentation(this WebApplication app)
        {
            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger(); // sirve /swagger/{documentName}/swagger.json

            app.MapScalarApiReference(options =>
            {
                options.WithTitle("Ecommerce API")
                       .WithOpenApiRoutePattern("/swagger/{documentName}/swagger.json");

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.AddDocument(
                        description.GroupName,
                        $"API {description.GroupName}" + (description.IsDeprecated ? " (deprecated)" : ""),
                        isDefault: !description.IsDeprecated);
                }

                // Misma constante que usa la definicion del esquema: un solo origen del nombre
                options.AddPreferredSecuritySchemes(JwtBearerDefaults.AuthenticationScheme);
            });

            return app;
        }
    }
}