using Specify.Autofac;
using Specify.Mocks;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            var config = new DefaultAutofacBootstrapper {MockFactory = new NullMockFactory()};
            var builder = new AutofacContainerFactory(config).Create();
            return new AutofacContainer(builder);
        }
    }
}