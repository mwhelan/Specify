using Specify.Autofac;

namespace Specify.IntegrationTests.DependencyResolvers
{
    public class AutofacDependencyResolverTests : DependencyResolverTests<AutofacDependencyResolver>
    {
        protected override AutofacDependencyResolver CreateSut()
        {
            return new AutofacDependencyResolver();
        }
    }
}