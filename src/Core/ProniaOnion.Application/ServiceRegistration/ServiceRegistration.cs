using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using ProniaOnion.Application.Validators;
using System.Reflection;

namespace ProniaOnion.Application.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddAplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining(typeof(CreateProductDtoValidator))); 
            return services;
        }
    }
}
