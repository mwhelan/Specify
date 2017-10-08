using Specify.Configuration;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class TinyApplicationContainerTests : ApplicationContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var config = new DefaultBootstrapper() { MockFactory = new NullMockFactory() };
            var container = new TinyContainerFactory(config).Create();
            return new TinyContainer(container);
        }
    }
}
