namespace Specify.IntegrationTests.TinyIoc
{
    public class DefaultDependencyResolverTests : DependencyResolverTests<DefaultDependencyResolver>
    {
        protected override DefaultDependencyResolver CreateSut()
        {
            return new DefaultDependencyResolver();
        }
    }
}
