using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace ApiTemplate.Api.Configuration
{
    /// <summary>
    /// Configures IoC containers - Autofac container and built-in service collection.
    /// Provides seam for tests to override these collections
    /// </summary>
    public class AppServiceProviderFactory : IServiceProviderFactory<ContainerBuilder>
    {
        private readonly Action<IServiceCollection> _servicesOverrides;
        private readonly Action<ContainerBuilder> _containerOverrides;

        public AppServiceProviderFactory(
            Action<ContainerBuilder> containerOverrides = null, Action<IServiceCollection> servicesOverrides = null)
        {
            _containerOverrides = containerOverrides ?? (builder => { });
            _servicesOverrides = servicesOverrides ?? (services => { });
        }

        // Register dependencies, populate the services from the collection, and build the container.
        // Note that Populate is basically a foreach to add things into Autofac that are in the services collection.
        // If you register // things in Autofac BEFORE Populate then the stuff in the ServiceCollection can override those things;
        // if you register // AFTER Populate those registrations can override things // in the ServiceCollection. Mix and match as needed.
        public ContainerBuilder CreateBuilder(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            _servicesOverrides(services);
            builder.Populate(services);

            builder.RegisterAssemblyModules(GetType().Assembly);

            _containerOverrides(builder);

            return builder;
        }

        public IServiceProvider CreateServiceProvider(ContainerBuilder containerBuilder)
        {
            if (containerBuilder == null) throw new ArgumentNullException(nameof(containerBuilder));

            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}