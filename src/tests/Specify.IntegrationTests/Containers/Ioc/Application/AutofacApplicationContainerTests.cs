using Specify.Autofac;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    public class AutofacApplicationContainerTests : ApplicationContainerTestsFor<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            var builder = new AutofacContainerFactory().Create(null);
            return new AutofacContainer(builder);
        }
    }
}