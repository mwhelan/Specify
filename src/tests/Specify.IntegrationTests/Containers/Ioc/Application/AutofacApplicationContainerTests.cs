using Autofac;
using Specify.Autofac;
using Specify.Mocks;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSpecify(new NullMockFactory());
            builder.RegisterInstance(new Dependency1()).As<IDependency1>();
            return new AutofacContainer(builder);
        }


    }
}