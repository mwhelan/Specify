using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests
{
    [TestFixture]
    public class SpecifyExtensionTests
    {
        [Test]
        public void Specification_instance_should_return_true_if_SpecificationFor()
        {
            new StubSpecificationFor()
                .IsSpecificationFor()
                .ShouldBe(true);
        }

        [Test]
        public void Specification_type_should_return_true_if_SpecificationFor()
        {
            typeof(StubSpecificationFor)
                .IsSpecificationFor()
                .ShouldBe(true);
        }

        [Test]
        public void Scenario_instance_should_return_false_if_not_SpecificationFor()
        {
            new StubScenarioFor()
                .IsSpecificationFor()
                .ShouldBe(false);;
        }

        [Test]
        public void Scenario_type_should_return_false_if_not_SpecificationFor()
        {
            typeof (StubScenarioFor)
                .IsSpecificationFor()
                .ShouldBe(false);
        }

        [Test]
        public void NonSpecification_type_should_return_false_if_not_Specification()
        {
            typeof(Dependency1)
                .IsSpecificationFor()
                .ShouldBe(false);
        }

        [Test]
        public void Scenario_instance_should_return_true_if_ScenarioFor()
        {
            new StubScenarioFor()
                .IsScenarioFor()
                .ShouldBe(true);
        }

        [Test]
        public void Scenario_type_should_return_true_if_ScenarioFor()
        {
            new StubScenarioFor()
                .IsScenarioFor()
                .ShouldBe(true);
        }

        [Test]
        public void Scenario_instance_should_return_false_if__not_ScenarioFor()
        {
            new StubSpecificationFor()
                .IsScenarioFor()
                .ShouldBe(false);;
        }

        [Test]
        public void Scenario_type_should_return_false_if__not_ScenarioFor()
        {
            typeof (StubSpecificationFor)
                .IsScenarioFor()
                .ShouldBe(false);
        }

        [Test]
        public void NonSpecification_type_should_return_false_if_not_Scenario()
        {
            typeof(Dependency1)
                .IsScenarioFor()
                .ShouldBe(false);
        }

        [Test]
        public void Nested_scenario_instance_should_return_false_if__not_ScenarioFor()
        {
            new ScenarioWithAllSupportedStepsInRandomOrder()
                .IsScenarioFor()
                .ShouldBe(true); ;
        }

        [Test]
        public void Nested_scenario_type_should_return_false_if__not_ScenarioFor()
        {
            typeof(ScenarioWithAllSupportedStepsInRandomOrder)
                .IsScenarioFor()
                .ShouldBe(true); ;
        }
    }
}
