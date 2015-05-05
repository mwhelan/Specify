using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.SutFactories
{

    public class SutFactoryAutofacContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<Dependency1>().As<IDependency1>();
            builder.RegisterType<Dependency2>().As<IDependency2>();
            var container = new AutofacContainer(builder);
            return new SutFactory<T>(container);
        }
    }
}
