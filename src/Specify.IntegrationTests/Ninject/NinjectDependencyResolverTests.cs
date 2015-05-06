using Specify.Ninject;

namespace Specify.IntegrationTests.Ninject
{

    public class NinjectDependencyResolverTests : DependencyResolverTests<NinjectApplicationContainer>
    {
        protected override NinjectApplicationContainer CreateSut()
        {
            var resolver = new NinjectApplicationContainer();
            return resolver;
        }
    }
}