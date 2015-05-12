using System.Linq;
using Specify.Configuration.Mocking;
using Specify.lib;

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
        }

        private void RegisterScenarioContainer()
        {
            var mockFactory = new MockDetector().FindAvailableMock();
            if (mockFactory == null)
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultScenarioContainer(c));
            }
            else
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultAutoMockingContainer(mockFactory()));
            }
        }
    }
}