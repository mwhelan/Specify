namespace Specify.Tests.Containers.DependencyResolvers
{
    using NUnit.Framework;

    using Shouldly;

    using Specify.Containers;
    using Specify.Tests.Stubs;

    [TestFixture]
    public abstract class DependencyResolverTests<T> where T : IDependencyResolver, new()
    {
        protected abstract T CreateSut();

        [Test]
        public void child_should_resolve_same_instance_as_parent()
        {
            var sut = this.CreateSut();
            sut.Register<IDependency1>(new Dependency1());
            var childContainer = sut.CreateChildContainer();
            sut.Resolve<IDependency1>()
                .ShouldBeSameAs(childContainer.Resolve<IDependency1>());
        }

        [Test]
        public void child_can_change_service_implementation_from_parent()
        {
            var sut = this.CreateSut();
            sut.Register<IDependency1>(new Dependency1());
            var childContainer = sut.CreateChildContainer();

            childContainer.Register<IDependency1>(new Dependency1());

            sut.Resolve<IDependency1>()
                .ShouldNotBeSameAs(childContainer.Resolve<IDependency1>());
        }

        [Test]
        public void changing_implementation_of_parent_singleton_in_child_does_not_change_parent_implementation()
        {
            var instance = new Dependency1();
            var sut = this.CreateSut();
            sut.Register<IDependency1>(instance);
            var childContainer = sut.CreateChildContainer();

            childContainer.Register<IDependency1>(new Dependency1());

            sut.Resolve<IDependency1>().ShouldBeSameAs(instance);
        }
    }
}
