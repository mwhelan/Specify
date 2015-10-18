using System;
using System.Linq;
using Specify.lib;
using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Mocks;

namespace Specify.Autofac
{
    public class AutofacApplicationContainer : AutofacScenarioContainer, IApplicationContainer
    {
        public AutofacApplicationContainer()
        {
            var assemblies = AssemblyTypeResolver.GetAllAssembliesFromAppDomain().ToArray();
            _containerBuilder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<>));
            _containerBuilder.RegisterAssemblyTypes(assemblies)
                .AsClosedTypesOf(typeof(ScenarioFor<,>));

            _containerBuilder.RegisterAssemblyModules(assemblies);

            var mockFactory = MockFactory ?? Host.Configuration.FindAvailableMock();
            if (mockFactory != null)
            {
                _containerBuilder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
                _containerBuilder.RegisterSource(new AutofacMockRegistrationHandler(mockFactory()));
            }
        }

        public IScenarioContainer CreateChildContainer()
        {
            return new AutofacScenarioContainer(Container.BeginLifetimeScope());
        }

        internal Func<IMockFactory> MockFactory { get; set; }
    }
}