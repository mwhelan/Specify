using Specify.Configuration;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class TinyContainerTests : IocContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(new NullMockFactory());
            return new TinyContainer(container);
        }
    }
}