using Specify.Autofac;

namespace Specify.IntegrationTests.Autofac
{
    public class AutofacDependencyResolverTests : DependencyResolverTests<AutofacApplicationContainer>
    {
        protected override AutofacApplicationContainer CreateSut()
        {
            return new AutofacApplicationContainer();
        }
    }
}