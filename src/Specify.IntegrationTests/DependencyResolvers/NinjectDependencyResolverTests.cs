using Ninject.Extensions.NamedScope;
using Specify.Ninject;

namespace Specify.IntegrationTests.DependencyResolvers
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