using Pacogroup.Ecommerce.Application.Validator;

namespace Pacogroup.Ecommerce.Services.WebApi.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<SingInDTOValidator>();
            services.AddTransient<SingUpDTOValidator>();

            return services;
        }
    }
}