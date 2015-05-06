using NUnit.Framework;
using Shouldly;
using Specify.Autofac;
using Specify.Tests.Stubs;

namespace Specify.IntegrationTests
{
    using global::Ninject;

    using Specify.Ninject;

    public class AutofacContainerTests : IocContainerTests<AutofacContainer>
    {
        protected override AutofacContainer CreateSut()
        {
            return new AutofacContainer();
        }

        [Test]
        public void cannot_resolve_concrete_types_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(false);
            sut.CanResolve<Dependency1>().ShouldBe(false);
        }
    }
    public class NinjectContainerTests : IocContainerTests<NinjectContainer>
    {
        protected override NinjectContainer CreateSut()
        {
            return new NinjectContainer(new StandardKernel());
        }

        [Test]
        public void cannot_resolve_concrete_types_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(false);
            sut.CanResolve<Dependency1>().ShouldBe(false);
        }
    }
    public class DefaultContainerTests : IocContainerTests<DefaultContainer>
    {
        protected override DefaultContainer CreateSut()
        {
            return new DefaultContainer();
        }

        [Test]
        public void can_resolve_concrete_types_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneConcreteConstructor>().ShouldBe(true);
            sut.CanResolve<Dependency1>().ShouldBe(true);
        }


    }
    [TestFixture]
    public abstract class IocContainerTests<T> where T : IScenarioContainer
    {
        protected abstract T CreateSut();
        [Test]
        public void cannot_resolve_service_implementations_that_are_not_registered()
        {
            var sut = CreateSut();
            sut.CanResolve<ConcreteObjectWithOneInterfaceConstructor>().ShouldBe(false);
        }

        [Test]
        public void can_register_service_implementation()
        {
            var sut = CreateSut();
            sut.Register<IDependency1, Dependency1>();
            sut.CanResolve<IDependency1>().ShouldBe(true);
        }

        [Test]
        public void RegisterService_should_register_singleton_lifetime()
        {
            var sut = CreateSut();
            sut.Register<IDependency2, Dependency2>();
            sut.Resolve<IDependency2>().ShouldBeSameAs(sut.Resolve<IDependency2>());
        }
    }
}
