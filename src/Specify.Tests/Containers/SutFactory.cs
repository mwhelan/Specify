using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;

namespace Specify.Tests.Containers
{
    [TestFixture]
    public class SutFactoryTests
    {
        [Test]
        public void should_use_container_to_create_sut()
        {
            var sut = CreateSut();
            sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();
            sut.Container.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut();
            var result = sut.SystemUnderTest<ConcreteObjectWithNoConstructor>();

            sut.SystemUnderTest<ConcreteObjectWithNoConstructor>().ShouldBeSameAs(result);
            sut.Container.Received(1).Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void RegisterType_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = CreateSut();
            sut.RegisterType<ConcreteObjectWithNoConstructor>();
            sut.Container.Received().RegisterType<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void RegisterType_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
            Should.Throw<InvalidOperationException>(() => sut.RegisterType<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register type after SUT is created.");
        }

        [Test]
        public void RegisterInstance_should_call_container_to_register_instance_if_SUT_not_set()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut();

            sut.RegisterInstance(instance);
            
            sut.Container.Received().RegisterInstance(instance);
        }

        [Test]
        public void RegisterInstance_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            sut.SystemUnderTest<ConcreteObjectWithMultipleConstructors>();
            Should.Throw<InvalidOperationException>(() => sut.RegisterInstance(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register instance after SUT is created.");
        }

        [Test]
        public void Resolve_should_call_container_resolve()
        {
            var sut = CreateSut();
            sut.Resolve<ConcreteObjectWithNoConstructor>();
            sut.Container.Received().Resolve<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void IsRegistered_generic_should_call_container_IsRegistered_generic()
        {
            var sut = CreateSut();
            sut.IsRegistered<ConcreteObjectWithNoConstructor>();
            sut.Container.Received().IsRegistered<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void IsRegistered_should_call_container_IsRegistered()
        {
            var sut = CreateSut();
            sut.IsRegistered(typeof(ConcreteObjectWithNoConstructor));
            sut.Container.Received().IsRegistered(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void should_dispose_container_when_disposed()
        {
            var sut = CreateSut();
            sut.Dispose();
            sut.Container.Received().Dispose();
        }

        private SutFactory CreateSut()
        {
            return new SutFactory(Substitute.For<IContainer>());
        }
    }
}
