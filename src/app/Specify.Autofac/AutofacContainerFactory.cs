using System;
using System.Linq;
using System.Reflection;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Autofac
{
    internal class AutofacContainerFactory
    {
        public ContainerBuilder Create(Func<IMockFactory> mockFactory)
        {
            var builder = new ContainerBuilder();
            RegisterScenarios(builder);
            RegisterScenarioContainer(builder, mockFactory);

            return builder;
        }

        private void RegisterScenarios(ContainerBuilder builder)
        {
            //var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            var assemblies = Assembly.GetEntryAssembly();
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            builder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));
        }

        private void RegisterScenarioContainer(ContainerBuilder builder, Func<IMockFactory> mockFactory)
        {
            if (mockFactory == null)
            {
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));
                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer", "TinyContainer");
            }
            else
            {
                var mockFactoryInstance = mockFactory.Invoke();
                builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                builder.RegisterSource(new AutofacMockRegistrationHandler(mockFactoryInstance));
                builder.Register<IContainer>(c => new AutofacContainer(c.Resolve<ILifetimeScope>().BeginLifetimeScope()));

                var mockFactoryName = mockFactoryInstance.GetType().Name;
                this.Log().DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "TinyMockingContainer", mockFactoryName);
            }
        }
    }
}