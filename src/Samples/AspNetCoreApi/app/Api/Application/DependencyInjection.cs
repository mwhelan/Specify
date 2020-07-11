using System.Linq;
using System.Reflection;
using ApiTemplate.Api.Application.Common.Behaviours;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceCollectionExtensions = AutoMapper.ServiceCollectionExtensions;

namespace ApiTemplate.Api.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServicesForApplicationProject(this IServiceCollection services)
        {
            ServiceCollectionExtensions.AddAutoMapper((IServiceCollection) services, cfg => cfg.Advanced.AllowAdditiveTypeMapCreation = true, Assembly.GetExecutingAssembly());
            services.AddFluentValidation(Assembly.GetExecutingAssembly());

            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(SqlDatabaseBehaviour<,>));

            return services;
        }

        public static IServiceCollection AddFluentValidation(this IServiceCollection services, Assembly assembly)
        {
            var validatorType = typeof(IValidator<>);

            var validatorTypes = assembly
                .GetExportedTypes()
                .Where(t => !t.IsAbstract && t.GetInterfaces().Any(i =>
                    i.IsGenericType &&
                    i.GetGenericTypeDefinition() == validatorType))
                .ToList();

            foreach (var validator in validatorTypes)
            {
                var requestType = validator.GetInterfaces()
                    .Where(i => i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IValidator<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First();

                var validatorInterface = validatorType
                    .MakeGenericType(requestType);

                services.AddTransient(validatorInterface, validator);
            }

            return services;
        }
    }
}
