using Autofac;
using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Scopes
{
    [TestFixture]
    public class AutofacSpecs
    {
        private global::Autofac.IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Dependency3>().As<IDependency3>().SingleInstance();    // Default
            builder.RegisterType<Dependency1>().As<IDependency1>().InstancePerDependency();
            builder.RegisterType<Dependency2>().As<IDependency2>().InstancePerLifetimeScope();
            return builder.Build();
        }

        [Test]
        public void Singleton_objects_should_be_same_in_root_and_child()
        {
            var root = CreateAutofacContainer();
            var rootSingleton = root.Resolve<IDependency3>();

            var child = root.BeginLifetimeScope();

            child.Resolve<IDependency3>().ShouldBeSameAs(rootSingleton);
        }

        [Test]
        public void Scoped_objects_should_be_different_from_root_instance_and_singleton_in_child()
        {
            var root = CreateAutofacContainer();
            var rootScoped = root.Resolve<IDependency2>();

            var child = root.BeginLifetimeScope();

            var childScoped = child.Resolve<IDependency2>();
            childScoped.ShouldNotBeSameAs(rootScoped);
            child.Resolve<IDependency2>().ShouldBeSameAs(childScoped);
        }

        [Test]
        public void can_override_singleton_objects_when_creating_child()
        {
            var root = CreateAutofacContainer();
            var rootSingleton = root.Resolve<IDependency3>();

            var child = root.BeginLifetimeScope(
                builder => builder.RegisterType<Dependency4>().As<IDependency3>().InstancePerLifetimeScope());

            var childSingleton = child.Resolve<IDependency3>();
            childSingleton.ShouldNotBeSameAs(rootSingleton);
            childSingleton.ShouldBeOfType<Dependency4>();
        }

    }
}
