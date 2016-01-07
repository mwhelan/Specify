using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests
{
    [TestFixture]
    public class ScenarioForTests
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
            sut.Container.SourceContainer.Received().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut();
            var result = sut.SUT;

            sut.SUT.ShouldBeSameAs(result);
            sut.Container.SourceContainer.Received(1).Get<ConcreteObjectWithNoConstructor>();
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
        public void Container_RegisterType_should_call_container_to_register_type_if_SUT_not_set()
        {
            var sut = CreateSut();
            sut.Container.Set<ConcreteObjectWithNoConstructor>();
            sut.Container.SourceContainer.Received().Set<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void Container_RegisterType_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            Should.Throw<InterfaceRegistrationException>(() => sut.Container.Set<ConcreteObjectWithNoConstructor>())
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }

        [Test]
        public void Container_RegisterInstance_should_call_container_to_register_instance_if_SUT_not_set()
        {
            var instance = new ConcreteObjectWithNoConstructor();
            var sut = CreateSut();

            sut.Container.Set(instance);

            sut.Container.SourceContainer.Received().Set(instance);
        }

        [Test]
        public void Container_RegisterInstance_should_throw_if_SUT_is_set()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            Should.Throw<InterfaceRegistrationException>(() => sut.Container.Set(new ConcreteObjectWithNoConstructor()))
                .Message.ShouldBe("Cannot register service Specify.Tests.Stubs.ConcreteObjectWithNoConstructor after SUT is created");
        }    
    
        [Test]
        public void Container_Get_should_call_container_resolve()
        {
            var sut = CreateSut();
            sut.Container.Get<ConcreteObjectWithNoConstructor>();
            sut.Container.SourceContainer.Received().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void unit_specification_title_should_be_class_name()
        {
            var sut = CreateSut();
            sut.Title.ShouldBe("Unit Scenario With All Supported Steps In Random Order");
        }

        [Test]
        public void scenario_title_should_be_class_name_only_if_scenario_is_zero()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UserStoryScenarioWithAllSupportedStepsInRandomOrder {Container = container};
            sut.Title.ShouldBe("User Story Scenario With All Supported Steps In Random Order");
        }
        [Test]
        public void scenario_title_should_be_number_and_class_name_if_number_greater_than_zero()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UserStoryScenarioWithAllSupportedStepsInRandomOrder {Container = container, Number = 3};
            sut.Title.ShouldBe("Scenario 03: User Story Scenario With All Supported Steps In Random Order");
        }
        private static UnitScenarioWithAllSupportedStepsInRandomOrder CreateSut()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrder { Container = container };
            return sut;
        }
    }
}