using Specify.Configuration;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class TinyApplicationContainerTests : ApplicationContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(new NullMockFactory());
            return new TinyContainer(container);
        }
    }
}
