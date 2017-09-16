using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            var builder = new AutofacContainerFactory().Create(new NullMockFactory());
            return new AutofacContainer(builder);
        }
    }
}