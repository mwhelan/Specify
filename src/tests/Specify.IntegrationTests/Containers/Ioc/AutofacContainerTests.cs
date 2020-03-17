using Autofac;
using Specify.Autofac;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class AutofacContainerTests : IocContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            var builder = new ContainerBuilder();
            ContainerBuilderExtensions.RegisterTypes(builder);

            return new AutofacContainer(builder);
        }
    }
}