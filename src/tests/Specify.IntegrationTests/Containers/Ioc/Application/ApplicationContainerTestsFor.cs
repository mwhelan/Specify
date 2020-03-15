using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests.Containers.Ioc.Application
{
    [TestFixture]
    public abstract class ApplicationContainerTestsFor<T> where T : IContainer
    {
        protected abstract T CreateSut();

        [Test]
        public void child_should_resolve_same_instance_as_parent()
        {
            var sut = this.CreateSut();
            var parentInstance = sut.Get<IDependency1>();

            sut.BeginScope();

            var childInstance = sut.Get<IDependency1>();
            childInstance.ShouldBeSameAs(parentInstance);
        }

        [Test]
        public void child_can_change_service_implementation_from_parent()
        {
            var sut = this.CreateSut();
            var instance1 = sut.Get<IDependency1>();

            var instance2 = new Dependency1 {Value = 10};
            sut.Set<IDependency1>(instance2);
            
            sut.BeginScope();

            sut.Get<IDependency1>()
                .ShouldNotBeSameAs(instance1);
        }

        [Test]
        public void changing_implementation_of_parent_singleton_in_child_does_not_change_parent_implementation()
        {
            var instance = new Dependency1();
            var sut = this.CreateSut();
            sut.Set<IDependency1>(instance);
            var childContainer = sut.Get<IContainer>();

            childContainer.Set<IDependency1>(new Dependency1());

            sut.Get<IDependency1>().ShouldBeSameAs(instance);
        }
    }
}
