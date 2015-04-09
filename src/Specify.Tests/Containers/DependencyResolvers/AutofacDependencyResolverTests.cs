namespace Specify.Tests.Containers.DependencyResolvers
{
    using Specify.Containers;

    public class AutofacDependencyResolverTests : DependencyResolverTests<AutofacDependencyResolver>
    {
        protected override AutofacDependencyResolver CreateSut()
        {
            return new AutofacDependencyResolver();
        }
    }
}