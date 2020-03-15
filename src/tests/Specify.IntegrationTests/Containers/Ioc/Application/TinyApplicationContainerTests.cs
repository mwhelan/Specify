using Specify.Configuration;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class TinyApplicationContainerTests : ApplicationContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(new NullMockFactory());
            container.Register<IDependency1>(new Dependency1());
            return new TinyContainer(container);
        }
    }
}
