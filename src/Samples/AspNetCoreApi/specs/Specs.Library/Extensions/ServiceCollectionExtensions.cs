using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Specs.Library.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Removes all registered <see cref="ServiceLifetime.Scoped"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
        /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
        /// <param name="services"></param>
        public static void SwapScoped<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Scoped))
            {
                var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Scoped).ToList();
                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            services.AddScoped(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Removes all registered <see cref="ServiceLifetime.Singleton"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
        /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
        /// <param name="services"></param>
        public static void SwapSingleton<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Singleton))
            {
                var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Singleton).ToList();
                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            services.AddSingleton(typeof(TService), typeof(TImplementation));
        }


        /// <summary>
        /// Removes all registered <see cref="ServiceLifetime.Transient"/> registrations of <see cref="TService"/> and adds in <see cref="TImplementation"/>.
        /// </summary>
        /// <typeparam name="TService">The type of service interface which needs to be placed.</typeparam>
        /// <typeparam name="TImplementation">The test or mock implementation of <see cref="TService"/> to add into <see cref="services"/>.</typeparam>
        /// <param name="services"></param>
        public static void SwapTransient<TService, TImplementation>(this IServiceCollection services)
            where TImplementation : class, TService
        {
            if (services.Any(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient))
            {
                var serviceDescriptors = services.Where(x => x.ServiceType == typeof(TService) && x.Lifetime == ServiceLifetime.Transient).ToList();
                foreach (var serviceDescriptor in serviceDescriptors)
                {
                    services.Remove(serviceDescriptor);
                }
            }

            services.AddTransient(typeof(TService), typeof(TImplementation));
        }
    }
}
