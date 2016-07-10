using System.Linq;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Autofac
{
    internal class AutofacContainerFactory
    {
        public ContainerBuilder Create(IMockFactory mockFactory)
        {
            var builder = new ContainerBuilder();
            RegisterScenarios(builder);
            RegisterScenarioContainer(builder, mockFactory);

            return builder;
        }

        private void RegisterScenarios(ContainerBuilder builder)
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));
        }

        private void RegisterScenarioContainer(ContainerBuilder builder, IMockFactory mockFactory)
        {
            if (mockFactory == null)
            {
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));
                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactory));
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));

                var mockFactoryName = mockFactory.MockProviderName;
                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", mockFactoryName);
            }
        }
    }
}