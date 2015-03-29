using Autofac;
using Autofac.Features.ResolveAnything;
using Specify.Containers;
using Specify.Examples.Autofac;

namespace Specify.Tests.Containers
{
    public class SpecificationContext_AutofacContainer_IntegrationTests : SpecificationContextIntegrationTests
    {
        protected override SpecificationContext<T> CreateSut<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterSource(new AnyConcreteTypeNotAlreadyRegisteredSource());
            var container = new AutofacContainer(builder);
            return new SpecificationContext<T>(container);
        }
    }
}
