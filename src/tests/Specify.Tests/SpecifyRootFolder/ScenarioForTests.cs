using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Exceptions;
using Specify.Tests.Stubs;
using TestStack.BDDfy;

namespace Specify.Tests.SpecifyRootFolder
{
    [TestFixture]
    public class ScenarioForTests
    {
        // TODO: Get this working with examples
        [Test]
        public void specify_steps_should_wrap_standard_BDDfy_conventions_with_examples()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples() { Container = container };

            sut
                .WithExamples(sut.Examples)
                .BDDfy();

            sut.Steps[0].ShouldBe("Constructor");

            for (int i = 1; i <= sut.Examples.Count; i++)
            {
                sut.Steps[i * 1].ShouldBe("BeginTestCase");             // Specify begin test case method
                sut.Steps[i * 2].ShouldBe("Setup");
                sut.Steps[i * 3].ShouldBe("EstablishContext");
                sut.Steps[i * 4].ShouldBe("GivenSomePrecondition");
                sut.Steps[i * 5].ShouldBe("AndGivenSomeOtherPrecondition");
                sut.Steps[i * 6].ShouldBe("WhenAction");
                sut.Steps[i * 7].ShouldBe("AndWhenAnotherAction");
                sut.Steps[i * 8].ShouldBe("ThenAnExpectation");
                sut.Steps[i * 9].ShouldBe("AndThenAnotherExpectation");
                sut.Steps[i * 10].ShouldBe("TearDown");                 
                sut.Steps[i * 11].ShouldBe("EndTestCase");              // Specify end test case method
            }
        }

        [Test]
        public void specify_steps_should_wrap_standard_BDDfy_conventions()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrder() {Container = container};

            sut.BDDfy();

            sut.Steps[0].ShouldBe("Constructor");

            sut.Steps[1].ShouldBe("BeginTestCase"); // Specify begin test case method
            sut.Steps[2].ShouldBe("Setup");
            sut.Steps[3].ShouldBe("EstablishContext");
            sut.Steps[4].ShouldBe("GivenSomePrecondition");
            sut.Steps[5].ShouldBe("AndGivenSomeOtherPrecondition");
            sut.Steps[6].ShouldBe("WhenAction");
            sut.Steps[7].ShouldBe("AndWhenAnotherAction");
            sut.Steps[8].ShouldBe("ThenAnExpectation");
            sut.Steps[9].ShouldBe("AndThenAnotherExpectation");
            sut.Steps[10].ShouldBe("TearDown");
            sut.Steps[11].ShouldBe("EndTestCase"); // Specify end test case method

        }

        [Test]
        public void should_use_context_to_create_sut()
        {
            var sut = CreateSut();
            var result = sut.SUT;
            sut.Container.SourceContainer.Received<IContainer>().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void sut_should_be_singleton()
        {
            var sut = CreateSut();
            var result = sut.SUT;

            sut.SUT.ShouldBeSameAs(result);
            sut.Container.SourceContainer.Received<IContainer>(1).Get<ConcreteObjectWithNoConstructor>();
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
            sut.Container.SourceContainer.Received<IContainer>().Set<ConcreteObjectWithNoConstructor>();
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

            sut.Container.SourceContainer.Received<IContainer>().Set(instance);
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
            sut.Container.SourceContainer.Received<IContainer>().Get<ConcreteObjectWithNoConstructor>();
        }

        [Test]
        public void scenario_title_should_be_full_class_name_including_parent_class_names()
        {
            var sut = new ParentScenario.ChildScenario.GrandChildScenario();
            sut.Title.ShouldBe("Parent Scenario + Child Scenario + Grand Child Scenario");
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
            sut.Title.ShouldBe("03: User Story Scenario With All Supported Steps In Random Order");
        }

        [Test]
        public void scenario_title_can_be_overridden_in_config()
        {
            var originalTitle = Config.ScenarioTitle;
            Config.ScenarioTitle = scenario => $"Scenario {scenario.Number:00}";
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());

            var sut = new UserStoryScenarioWithAllSupportedStepsInRandomOrder { Container = container, Number = 3 };

            sut.Title.ShouldBe("Scenario 03");
            Config.ScenarioTitle = originalTitle;
        }

        [TestCase(typeof(StubScenarioWithNumberOverridden))]
        [TestCase(typeof(StubScenarioWithNumberSetCtor))]
        public void scenario_number_can_be_set_in_scenario(Type scenarioType)
        {
            scenarioType.Create<IScenario>().Number.ShouldBe(29);
        }

        private static UnitScenarioWithAllSupportedStepsInRandomOrder CreateSut()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrder { Container = container };
            return sut;
        }
    }
}