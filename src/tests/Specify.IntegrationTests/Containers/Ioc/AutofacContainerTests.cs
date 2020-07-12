using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class AutofacContainerTests : IocContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return ContainerFactory.CreateAutofacContainer<NullMockFactory>();
        }
    }
}