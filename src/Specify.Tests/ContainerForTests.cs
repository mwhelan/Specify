using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    [TestFixture]
    public class ContainerForTests
    {
        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;
            sut.SourceContainer.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;

            sut.SystemUnderTest.ShouldBeSameAs(result);
            sut.SourceContainer.Received(1).Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            var original = sut.SystemUnderTest;

            sut.SystemUnderTest = instance;

            sut.SystemUnderTest.ShouldBeSameAs(instance);
            sut.SystemUnderTest.ShouldNotBeSameAs(original);
        }

        [Test]
        public void RegisterType_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Set<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().Register<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void RegisterType_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }

        [Test]
        public void RegisterService_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Set<IDependency1, Dependency1>();
            sut.SourceContainer.Received().Register<IDependency1, Dependency1>();
        }

        [Test]
        public void RegisterService_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set<IDependency1, Dependency1>())
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.Dependency1 after SUT is created");
        }

        [Test]
        public void RegisterInstance_should_call_container_to_register_instance_if_SUT_not_set()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();

            sut.Set(instance);

            sut.SourceContainer.Received().Register(instance);
        }

        [Test]
        public void RegisterInstance_should_throw_if_SUT_is_set()
        {
            var sut = this.CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InterfaceRegistrationException>(() => sut.Set(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }

        [Test]
        public void Get_generic_should_call_container_resolve_generic()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void Get_generic_should_throw_InterfaceResolutionException_if_container_throws()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Resolve<ConcreteObjectWithNoConstructor>(null)
                .Returns(x => { throw new Exception(); });

            Action action = () => sut.Get<ConcreteObjectWithNoConstructor>();

            Should.Throw<InterfaceResolutionException>(action)
                .Message.ShouldBe("Failed to resolve an implementation of Specify.Tests.Stubs.ConcreteObjectWithNoConstructor.");
        }

        [Test]
        public void Get_should_call_container_resolve()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received().Resolve(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void Get_should_throw_InterfaceResolutionException_if_container_throws()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Resolve(typeof (ConcreteObjectWithNoConstructor), null)
                .Returns(x => { throw new Exception(); });

            Action action = () => sut.Get(typeof(ConcreteObjectWithNoConstructor));

            Should.Throw<InterfaceResolutionException>(action)
                .Message.ShouldBe("Failed to resolve an implementation of Specify.Tests.Stubs.ConcreteObjectWithNoConstructor.");
        }

        [Test]
        public void IsRegistered_generic_should_call_container_IsRegistered_generic()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.IsRegistered<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().CanResolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void IsRegistered_should_call_container_IsRegistered()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.IsRegistered(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received().CanResolve(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void should_dispose_container_when_disposed()
        {
            var sut = this.CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Dispose();
            sut.SourceContainer.Received().Dispose();
        }

        private ContainerFor<T> CreateSut<T>() where T : class
        {
            return new ContainerFor<T>(Substitute.For<IContainer>());
        }
    }
}
