using System.Linq;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Configuration.Examples;
using Specify.Containers;
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
            RegisterTypes(builder);
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
            builder.Register<IContainerRoot>(c => new AutofacContainer(c.Resolve<ILifetimeScope>())).SingleInstance();

            if (mockFactory.GetType() == typeof(NullMockFactory))
            {
                nameof(ContainerBuilderExtensions).Log().DebugFormat("Registered {ScenarioContainer} for IContainer with no mock factory", "AutofacContainer");
            }
            else
            {
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactory));

                nameof(ContainerBuilderExtensions).Log().DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "AutofacMockingContainer", mockFactory.MockProviderName);
            }
        }

        internal static void RegisterTypes(this ContainerBuilder builder)
        {
            builder.RegisterType<ExampleScope>().As<IExampleScope>();
            builder.RegisterType<AutofacChildContainerBuilder>().As<IChildContainerBuilder>();
        }
    }
}