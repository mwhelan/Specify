namespace Specify.Tests.Containers.DependencyResolvers
{
    public class DefaultDependencyResolverTests : DependencyResolverTests<DefaultDependencyResolver>
    {
        protected override DefaultDependencyResolver CreateSut()
        {
            return new DefaultDependencyResolver();
        }
    }
}
