using Specify.Ninject;

namespace Specify.IntegrationTests.Ninject
{

    public class NinjectDependencyResolverTests : DependencyResolverTests<NinjectDependencyResolver>
    {
        protected override NinjectDependencyResolver CreateSut()
        {
            var resolver = new NinjectDependencyResolver();
            return resolver;
        }
    }
}