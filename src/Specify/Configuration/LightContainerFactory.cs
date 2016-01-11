using System;
using System.Collections.Generic;
using System.Linq;
using Specify.lib;
using Specify.LightInject;
using Specify.Logging;
using Specify.Mocks;

namespace Specify.Configuration
{
    internal class LightContainerFactory
    {
        public ServiceContainer Create(Func<IMockFactory> mockFactory)
        {
            var container = new ServiceContainer();
            RegisterScenarios(container);
            RegisterScenarioContainer(container, mockFactory);
            return container;
        }

        private void RegisterScenarios(ServiceContainer container)
        {
            var scenarios = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario() && !type.IsAbstract)
                .ToList();

            foreach (var scenario in scenarios)
            {
                container.Register(scenario);
            }

            this.Log().DebugFormat("Registered {RegisteredScenarioCount} Scenarios", scenarios.Count);
        }

        private void RegisterScenarioContainer(IServiceContainer container, Func<IMockFactory> mockFactory)
        {
            if (mockFactory == null)
            {
                //container.Register<IContainer>(factory => new LightContainer((IServiceContainer)factory, factory.BeginScope()));
                container.Register<IContainer>(factory => CreateChildContainer(factory, false));
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer", "LightContainer");
            }
            else
            {
                var mockFactoryInstance = mockFactory.Invoke();
                container.RegisterFallback(CanResolveMock, request => mockFactoryInstance.CreateMock(request.ServiceType));
                //container.Register<IContainer>(factory => new LightContainer((IServiceContainer)factory, factory.BeginScope()));
                container.Register<IContainer>(factory => CreateChildContainer(factory, true));
                var mockFactoryName = mockFactory().GetType().Name;
                this.Log()
                    .DebugFormat("Registered {ScenarioContainer} for IContainer with mock factory {MockFactory}", "LightContainer", mockFactoryName);
            }
        }

        private bool CanResolveMock(Type serviceType, string name)
        {
            bool cannotResolve = (serviceType == null ||
                    !serviceType.IsInterface ||
                    serviceType.IsGenericType &&
                    serviceType.GetGenericTypeDefinition() == typeof (IEnumerable<>) ||
                    serviceType.IsArray);
            return !cannotResolve;
        }

        internal static IContainer CreateChildContainer(IServiceFactory factory, bool isMocking)
        {
            var serviceContainer = factory as IServiceContainer;
            return isMocking
                ? new LightMockingContainer(serviceContainer, serviceContainer.BeginScope())
                : new LightContainer(serviceContainer, serviceContainer.BeginScope());
        }
    }
}