using Specify.Configuration.Mocking;

namespace Specify
{
    public class DefaultDependencyResolver : DefaultContainer, IDependencyResolver
    {
        public DefaultDependencyResolver()
        {
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            Container.AutoRegister();
            var mockFactory = new MockDetector().FindAvailableMock();
            if (mockFactory == null)
            {
                Container.Register((c, p) => new DefaultContainer(c));
            }
            else
            {
                Container.Register<IScenarioContainer>((c, p) => new DefaultAutoMockingContainer(mockFactory()));
            }
        }

        public IScenarioContainer CreateChildContainer()
        {
            return new DefaultContainer(Container.GetChildContainer());
        }
    }
}