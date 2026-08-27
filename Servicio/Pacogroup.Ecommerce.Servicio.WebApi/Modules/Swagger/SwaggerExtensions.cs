using System.Reflection;
using Microsoft.OpenApi;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                // Estadar para la documentacion de Swagger con OpenApi
                c.SwaggerDoc(
                "v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Pacagroup Technology Services API Market",
                    Description = "A simple example ASP.NET Core Web API. ",
                    TermsOfService = new Uri("https://pacagroup.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Lorenzo.Lagos",
                        Email = "lagosariasa343@gmail.com",
                        Url = new Uri("https://pacagroup.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://pacagroup.com/licence")
                    }
                });

                // Inclusion del archivo xml generado automaticamente
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.EnableAnnotations();
            });

            return services;
        }

    }
}