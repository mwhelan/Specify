namespace Specify.Tests.Containers.DependencyResolvers
{
    using Specify.Examples.Ninject;

    public class NinjectDependencyResolverTests : DependencyResolverTests<NinjectDependencyResolver>
    {
        protected override NinjectDependencyResolver CreateSut()
        {
            return new NinjectDependencyResolver();
        }
    }
}