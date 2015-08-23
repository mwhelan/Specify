using System.Linq;
using Specify.Configuration.Mocking;
using Specify.lib;
using Specify.Logging;

namespace Specify
{
    public class DefaultApplicationContainer : DefaultScenarioContainer, IApplicationContainer
    {
        public DefaultApplicationContainer()
        {
            ConfigureContainer();
        }

        public IScenarioContainer CreateChildContainer()
        {
            return new DefaultScenarioContainer(Container.GetChildContainer());
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
            var mockFactory = new MockDetector().FindAvailableMock();
            if (mockFactory == null)
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultScenarioContainer(c));
                this.Log().DebugFormat("Registered {ScenarioContainer} for IScenarioContainer", "DefaultScenarioContainer");
            }
            else
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultAutoMockingContainer(mockFactory()));
                var mockFactoryName = mockFactory().GetType().Name;
                this.Log().DebugFormat("Registered {ScenarioContainer} for IScenarioContainer with mock factory {MockFactory}", "DefaultAutoMockingContainer", mockFactoryName);
            }
        }
    }
}