using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Containers;
using Specify.Examples.Autofac;
using Specify.Tests.Containers;

namespace Specify.Tests.Examples
{
    public class SutFactoryAutofacContainerIntegrationTests : SutFactoryIntegrationTests
    {
        protected override SutFactory<T> CreateSut<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            var container = new AutofacContainer(builder);
            return new SutFactory<T>(container);
        }
    }
}
