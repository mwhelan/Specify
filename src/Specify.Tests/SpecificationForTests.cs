using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Containers;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests
{
    [TestFixture]
    public class SpecificationForTests
    {
        [Test]
        public void specification_step_order_should_follow_standard_BDDfy_conventions()
        {
            var sut = CreateSut();

            sut.BDDfy();

            sut.Steps[0].ShouldBe("Constructor");
            sut.Steps[1].ShouldBe("Setup");
            sut.Steps[2].ShouldBe("EstablishContext");
            sut.Steps[3].ShouldBe("GivenSomePrecondition");
            sut.Steps[4].ShouldBe("AndGivenSomeOtherPrecondition");
            sut.Steps[5].ShouldBe("WhenAction");
            sut.Steps[6].ShouldBe("AndWhenAnotherAction");
            sut.Steps[7].ShouldBe("ThenAnExpectation");
            sut.Steps[8].ShouldBe("AndThenAnotherExpectation");
            sut.Steps[9].ShouldBe("TearDown");
        }

        [Test]
        public void should_use_context_to_create_sut()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            sut.Context.Container.Received().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut();
            var result = sut.SUT;

            sut.SUT.ShouldBeSameAs(result);
            sut.Context.Container.Received(1).Get<ConcreteObjectWithNoConstructor>();
        }
        [Test]
        public void should_be_able_to_set_sut_independently()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut();
            var original = sut.SUT;

            sut.SUT = instance;

            sut.SUT.ShouldBeSameAs(instance);
            sut.SUT.ShouldNotBeSameAs(original);
        }

        [Test]
        public void Set_type_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = CreateSut();
            sut.Set<ConcreteObjectWithNoConstructor>();
            sut.Context.Container.Received().RegisterType<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void Set_type_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            Should.Throw<InvalidOperationException>(() => sut.Set<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register type after SUT is created.");
        }

        [Test]
        public void Set_instance_should_call_container_to_register_instance_if_SUT_not_set()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut();

            sut.Set(instance);

            sut.Context.Container.Received().RegisterInstance(instance);
        }

        [Test]
        public void Set_instance_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            Should.Throw<InvalidOperationException>(() => sut.Set(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register instance after SUT is created.");
        }    
    
        [Test]
        public void Get_should_call_container_resolve()
        {
            var sut = CreateSut();
            sut.Get<ConcreteObjectWithNoConstructor>();
            sut.Context.Container.Received().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void specification_title_should_be_sut_name()
        {
            var sut = CreateSut();
            sut.Title.ShouldBe("ConcreteObjectWithNoConstructor");
        }

        [Test]
        public void scenario_title_should_be_class_name_only_if_scenario_is_zero()
        {
            var container = new SutFactory<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new ScenarioWithAllSupportedStepsInRandomOrder {Context = container};
            sut.Title.ShouldBe("Scenario With All Supported Steps In Random Order");
        }
        [Test]
        public void scenario_title_should_be_number_and_class_name_if_number_greater_than_zero()
        {
            var container = new SutFactory<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new ScenarioWithAllSupportedStepsInRandomOrder {Context = container, Number = 3};
            sut.Title.ShouldBe("Scenario 03: Scenario With All Supported Steps In Random Order");
        }
        private static SpecWithAllSupportedStepsInRandomOrder CreateSut()
        {
            var container = new SutFactory<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new SpecWithAllSupportedStepsInRandomOrder { Context = container };
            return sut;
        }
    }
    public class SutFactoryTests
    {
        [Test]
        public void IsRegistered_should_call_container_IsRegistered()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.IsRegistered(typeof(ConcreteObjectWithNoConstructor));
            sut.Container.Received().IsRegistered(typeof(ConcreteObjectWithNoConstructor));
        }

        [Test]
        public void should_dispose_container_when_disposed()
        {
            var sut = CreateSut<ConcreteObjectWithNoConstructor>();
            sut.Dispose();
            sut.Container.Received().Dispose();
        }

        private SutFactory<T> CreateSut<T>() where T : class
        {
            return new SutFactory<T>(Substitute.For<IContainer>());
        }
    }

}