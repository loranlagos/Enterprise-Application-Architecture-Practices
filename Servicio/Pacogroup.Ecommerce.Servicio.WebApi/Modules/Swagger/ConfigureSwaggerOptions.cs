using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    /// <summary>
    /// Clase donde se configuran cada uno de los documentos swagger opor version
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider; //Interfaz que nos permite descubrir todas las versiones configuradas dentro de la api

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Confugura un nuevo metodo swagger por cada version descubierta por la interfaz IApiVersionDescriptionProvider de la API
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
            return new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(), //obtencion de la version desde la descripcion de la version de la API
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
            };
        }
    }
}