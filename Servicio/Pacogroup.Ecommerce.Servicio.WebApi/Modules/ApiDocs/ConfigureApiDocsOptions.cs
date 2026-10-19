using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    /// <summary>
    /// Configura un documento OpenAPI por cada version de la API descubierta.
    /// </summary>
    public class ConfigureApiDocsOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider; //Interfaz que nos permite descubrir todas las versiones configuradas dentro de la api

        public ConfigureApiDocsOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Configura un nuevo metodo por cada version descubierta por la interfaz IApiVersionDescriptionProvider de la API
        /// </summary>
        /// <param name="options"></param>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var descripcion in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(descripcion.GroupName, CreateInfoForApiVersion(descripcion));
            }
        }

        /// <summary>
        /// Crea la documentacion de la API segun su version
        /// </summary>
        /// <param name="description"></param>x
        /// <returns></returns>
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Ecommerce API",
                Description = """
                    Backend API built with **ASP.NET Core (.NET 10)** following a layered architecture.

                    - JWT authentication
                    - API versioning (v1 deprecated, v2 current)
                    - FluentValidation, Dapper, Serilog and health checks

                    Practice project based on an enterprise backend architecture course.
                    """,
                Contact = new OpenApiContact
                {
                    Name = "Lorenzo Lagos",
                    Url = new Uri("https://github.com/loranlagos")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "\n\n**This API version is deprecated.**";
            }

            return info;
        }
    }
}