using Specify.Configuration.Mocking;

namespace Specify
{
    public class DefaultApplicationContainer : DefaultScenarioContainer, IApplicationContainer
    {
        public DefaultApplicationContainer()
        {
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            Container.AutoRegister();
            var mockFactory = new MockDetector().FindAvailableMock();
            if (mockFactory == null)
            {
                Container.Register((c, p) => new DefaultScenarioContainer(c));
            }
            else
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultAutoMockingContainer(mockFactory()));
            }
        }

        public IScenarioContainer CreateChildContainer()
        {
            return new DefaultScenarioContainer(Container.GetChildContainer());
        }
    }
}