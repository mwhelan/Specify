using Specify.Autofac;

namespace Specify.Tests.Containers.DependencyResolvers
{
    public class AutofacDependencyResolverTests : DependencyResolverTests<AutofacDependencyResolver>
    {
        protected override AutofacDependencyResolver CreateSut()
        {
            return new AutofacDependencyResolver();
        }
    }
}