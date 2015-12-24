using Specify.Configuration;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class TinyApplicationContainerTests : ApplicationContainerTestsFor<TinyContainer>
    {
        protected override TinyContainer CreateSut()
        {
            var container = new TinyContainerFactory().Create(null);
            return new TinyContainer(container);
        }
    }
}
