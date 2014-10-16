using Autofac;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers
{
    public class AutofacDependencyScopeIntegrationSpecs : SpecificationContextIntegrationSpecs
    {
        protected override SpecificationContext<TSut> CreateSut<TSut>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Dependency1>().As<IDependency1>().InstancePerLifetimeScope();
            builder.RegisterType<Dependency2>().As<IDependency2>().InstancePerLifetimeScope();
            builder.RegisterType<Dependency3>().As<IDependency3>().InstancePerLifetimeScope();
            builder.RegisterType<ConcreteObjectWithNoConstructor>().InstancePerLifetimeScope();
            builder.RegisterType<ConcreteObjectWithMultipleConstructors>().InstancePerLifetimeScope();
            var container = builder.Build();
            var scope = new AutofacDependencyScope(container);
            return new SpecificationContext<TSut>(scope);
        }
    }
}