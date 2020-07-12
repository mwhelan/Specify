using Autofac;
using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return ContainerFactory.CreateAutofacContainer<NullMockFactory>();
        }
    }
}