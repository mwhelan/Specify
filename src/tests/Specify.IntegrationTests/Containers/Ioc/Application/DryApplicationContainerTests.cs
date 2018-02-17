using Specify.Microsoft.DependencyInjection;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class DryApplicationContainerTests : ApplicationContainerTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            var container = new DryContainerFactory().Create(new NullMockFactory());
            return new DryContainer(container);
        }
    }
}