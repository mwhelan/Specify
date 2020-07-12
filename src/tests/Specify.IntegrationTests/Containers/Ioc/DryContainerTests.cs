using DryIoc;
using Specify.Containers;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class DryContainerTests : IocContainerTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            var container = new DryContainerFactory().Create(new NullMockFactory());
            return new DryContainer(container);
        }
    }
}
