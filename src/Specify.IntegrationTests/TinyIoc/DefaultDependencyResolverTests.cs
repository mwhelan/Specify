namespace Specify.IntegrationTests.TinyIoc
{
    public class DefaultDependencyResolverTests : DependencyResolverTests<DefaultApplicationContainer>
    {
        protected override DefaultApplicationContainer CreateSut()
        {
            return new DefaultApplicationContainer();
        }
    }
}
