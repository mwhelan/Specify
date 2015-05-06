using Specify.Autofac;

namespace Specify.IntegrationTests.Autofac
{
    public class AutofacDependencyResolverTests : DependencyResolverTests<AutofacDependencyResolver>
    {
        protected override AutofacDependencyResolver CreateSut()
        {
            return new AutofacDependencyResolver();
        }
    }
}