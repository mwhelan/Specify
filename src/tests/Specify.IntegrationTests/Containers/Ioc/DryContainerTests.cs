using DryIoc;
using Specify.Microsoft.DependencyInjection;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class DryContainerTests : IocContainerTestsFor<DryContainer>
    {
        protected override DryContainer CreateSut()
        {
            return new DryContainer(new Container());
        }
    }
}
