using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            //el documento swagger estatico ha sido migrado a la nueva logica de documentacion por version
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(c =>
            {
                // IMPORTANTE:
                // Incluir en cada swagger solo los endpoints pertenecientes a esa versión.
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

    }
}