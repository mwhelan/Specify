namespace Specify.IntegrationTests.DependencyResolvers
{
    public class DefaultDependencyResolverTests : DependencyResolverTests<DefaultDependencyResolver>
    {
        protected override DefaultDependencyResolver CreateSut()
        {
            return new DefaultDependencyResolver();
        }
    }
}
