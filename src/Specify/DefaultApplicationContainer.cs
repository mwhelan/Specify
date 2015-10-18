using System;
using System.Linq;
using Specify.Configuration.Mocking;
using Specify.lib;
using Specify.Logging;
using Specify.Mocks;

namespace Specify
{
    public class DefaultApplicationContainer : DefaultScenarioContainer, IApplicationContainer
    {
        private Func<IMockFactory> _mockFactory;
         
        public DefaultApplicationContainer()
        {
            ConfigureContainer();
        }

        public IScenarioContainer CreateChildContainer()
        {
            return _mockFactory == null 
                ? new DefaultScenarioContainer(Container.GetChildContainer()) 
                : new DefaultAutoMockingContainer(_mockFactory(), Container.GetChildContainer());
        }

        private void ConfigureContainer()
        {
            RegisterScenarios();
            RegisterScenarioContainer();
        }

        private void RegisterScenarios()
        {
            var scenarios = AssemblyTypeResolver
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsScenario() && !type.IsAbstract);

            Container.RegisterMultiple<IScenario>(scenarios);
            this.Log().DebugFormat("Registered {RegisteredScenarioCount} Scenarios", scenarios.Count());
        }

        private void RegisterScenarioContainer()
        {
            _mockFactory = new MockDetector().FindAvailableMock();
            if (_mockFactory == null)
            {
                this.Log().DebugFormat("Registered {ScenarioContainer} for IScenarioContainer", "DefaultScenarioContainer");
            }
            else
            {
                var mockFactoryName = _mockFactory().GetType().Name;
                this.Log().DebugFormat("Registered {ScenarioContainer} for IScenarioContainer with mock factory {MockFactory}", "DefaultAutoMockingContainer", mockFactoryName);
            }
        }
    }
}