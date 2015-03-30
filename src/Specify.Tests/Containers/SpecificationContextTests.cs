using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers
{
    [TestFixture]
    public class SpecificationContextTests
    {
        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;
            sut.SourceContainer.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var result = sut.SystemUnderTest;

            sut.SystemUnderTest.ShouldBeSameAs(result);
            sut.SourceContainer.Received(1).Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            var original = sut.SystemUnderTest;

            sut.SystemUnderTest = instance;

            sut.SystemUnderTest.ShouldBeSameAs(instance);
            sut.SystemUnderTest.ShouldNotBeSameAs(original);
        }

        [Test]
        public void RegisterType_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Register<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().Register<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void RegisterType_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register type after SUT is created.");
        }

        [Test]
        public void RegisterService_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Register<IDependency1, Dependency1>();
            sut.SourceContainer.Received().Register<IDependency1,Dependency1>();
        }

        [Test]
        public void RegisterService_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register<IDependency1, Dependency1>())
                .Message.ShouldBe("Cannot register service after SUT is created.");
        }

        [Test]
        public void RegisterInstance_should_call_container_to_register_instance_if_SUT_not_set()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();

            sut.Register(instance);
            
            sut.SourceContainer.Received().Register(instance);
        }

        [Test]
        public void RegisterInstance_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut<ConcreteObjectWithMultipleConstructors>();
            var result = sut.SystemUnderTest;
            Should.Throw<InvalidOperationException>(() => sut.Register(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register instance after SUT is created.");
        }

        [Test]
        public void Resolve_generic_should_call_container_resolve_generic()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void Resolve_should_call_container_resolve()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Get(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received().Resolve(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void IsRegistered_generic_should_call_container_IsRegistered_generic()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.IsRegistered<ConcreteObjectWithNoConstructor>();
            sut.SourceContainer.Received().CanResolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void IsRegistered_should_call_container_IsRegistered()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.IsRegistered(typeof(ConcreteObjectWithNoConstructor));
            sut.SourceContainer.Received().CanResolve(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void should_dispose_container_when_disposed()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Dispose();
            sut.SourceContainer.Received().Dispose();
        }

        private SutFactory<T> CreateSut<T>() where T : class
        {
            return new SutFactory<T>(Substitute.For<IContainer>());
        }
    }
}
