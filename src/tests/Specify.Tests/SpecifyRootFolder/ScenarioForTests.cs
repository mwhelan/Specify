using System;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Specify.Configuration.StepScanners;
using Specify.Tests.Stubs;
using TestStack.BDDfy;
using TestStack.BDDfy.Configuration;

namespace Specify.Tests.SpecifyRootFolder
{
    [TestFixture]
    public class ScenarioForTests
    {
        [Test]
        public void specify_steps_should_wrap_standard_BDDfy_conventions_with_examples()
        {
            Configurator.Scanners.ExecutableAttributeScanner.Disable();
            Configurator.Scanners.Add(() => new SpecifyExecutableAttributeStepScanner());

            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrderWithExamples() { Container = container };

            sut
                .WithExamples(sut.Examples)
                .BDDfy(sut.Title);

            sut.Steps[0].ShouldBe("Constructor");

            for (int i = 0; i < sut.Examples.Count; i++)
            {
                //sut.Steps[(i*11)+1].ShouldBe("BeginTestCase");             // Specify begin test case method
                sut.Steps[(i*9)+1].ShouldBe("Setup");
                sut.Steps[(i*9)+2].ShouldBe("EstablishContext");
                sut.Steps[(i*9)+3].ShouldBe("GivenSomePrecondition");
                sut.Steps[(i*9)+4].ShouldBe("AndGivenSomeOtherPrecondition");
                sut.Steps[(i*9)+5].ShouldBe("WhenAction");
                sut.Steps[(i*9)+6].ShouldBe("AndWhenAnotherAction");
                sut.Steps[(i*9)+7].ShouldBe("ThenAnExpectation");
                sut.Steps[(i*9)+8].ShouldBe("AndThenAnotherExpectation");
                sut.Steps[(i*9)+9].ShouldBe("TearDown");
                //sut.Steps[(i*11)+11].ShouldBe("EndTestCase");              // Specify end test case method
            }
        }

        [Test]
        public void specify_steps_should_wrap_standard_BDDfy_conventions()
        {
            var container = new ContainerFor<ConcreteObjectWithNoConstructor>(Substitute.For<IContainer>());
            var sut = new UnitScenarioWithAllSupportedStepsInRandomOrder() {Container = container};

            sut.BDDfy();

            sut.Steps[0].ShouldBe("Constructor");

            //sut.Steps[1].ShouldBe("BeginTestCase"); // Specify begin test case method
            sut.Steps[1].ShouldBe("Setup");
            sut.Steps[2].ShouldBe("EstablishContext");
            sut.Steps[3].ShouldBe("GivenSomePrecondition");
            sut.Steps[4].ShouldBe("AndGivenSomeOtherPrecondition");
            sut.Steps[5].ShouldBe("WhenAction");
            sut.Steps[6].ShouldBe("AndWhenAnotherAction");
            sut.Steps[7].ShouldBe("ThenAnExpectation");
            sut.Steps[8].ShouldBe("AndThenAnotherExpectation");
            sut.Steps[9].ShouldBe("TearDown");
            //sut.Steps[11].ShouldBe("EndTestCase"); // Specify end test case method

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