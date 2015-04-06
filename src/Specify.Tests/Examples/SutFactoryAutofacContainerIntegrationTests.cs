using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Containers;
using Specify.Tests.Containers.SutFactory;
using Specify.Tests.Stubs;

namespace Specify.Tests.Examples
{
    public class SutFactoryAutofacContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            builder.RegisterType<Dependency1>().As<IDependency1>();
            builder.RegisterType<Dependency2>().As<IDependency2>();
            var container = new IocContainer(builder);
            return new SutFactory<T>(container);
        }
    }
}
