using System.Linq;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Autofac
{
    public static class ContainerBuilderExtensions
    {
        public static void RegisterSpecify(this ContainerBuilder builder, IMockFactory mockFactory = null)
        {
            if (builder == null)
            {
                builder = new ContainerBuilder();
            }

            if (mockFactory == null)
            {
                mockFactory = new NullMockFactory();
            }

            RegisterScenarios(builder);
            RegisterScenarioContainer(builder, mockFactory);
        }

        private static void RegisterScenarios(ContainerBuilder builder)
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));
        }

        private static void RegisterScenarioContainer(ContainerBuilder builder, IMockFactory mockFactory)
        {
            if (mockFactory.GetType() == typeof(NullMockFactory))
            {
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));
                nameof(ContainerBuilderExtensions).Log().DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactory));
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));

                nameof(ContainerBuilderExtensions).Log().DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", mockFactory.MockProviderName);
            }
        }
    }
}