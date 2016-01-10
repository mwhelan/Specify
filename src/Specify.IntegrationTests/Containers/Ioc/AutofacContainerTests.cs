using Specify.Autofac;

namespace Specify.IntegrationTests.Containers.Ioc
{
    public class AutofacContainerTests : IocContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return new AutofacContainer();
        }
    }
}