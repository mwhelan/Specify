using NUnit.Framework;
using Shouldly;
using Specify.Tests.Stubs;

namespace Specify.Tests.SpecifyRootFolder
{
    [TestFixture]
    public class ScenarioExtensionsTests
    {
        [Test]
        public void IsScenario_type_should_return_true_if_concrete_scenario()
        {
            typeof(StubUnitScenario).IsScenario().ShouldBe(true);
            typeof(StubUserStoryScenario).IsScenario().ShouldBe(true);
        }

        [Test]
        public void IsScenario_type_should_return_false_if_not_concrete_scenario()
        {
            typeof(Dependency1).IsScenario().ShouldBe(false);
            typeof(IScenario).IsScenario().ShouldBe(false);
        }

        [Test]
        public void UnitScenario_instance_should_return_true_if_UnitScenario()
        {
            new StubUnitScenario()
                .IsUnitScenario()
                .ShouldBe(true);
        }

        [Test]
        public void UnitScenario_type_should_return_true_if_UnitScenario()
        {
            typeof(StubUnitScenario)
                .IsUnitScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_instance_should_return_false_if_not_UnitScenario()
        {
            new StubUserStoryScenario()
                .IsUnitScenario()
                .ShouldBe(false);;
        }

        [Test]
        public void StoryScenario_type_should_return_false_if_not_UnitScenario()
        {
            typeof (StubUserStoryScenario)
                .IsUnitScenario()
                .ShouldBe(false);
        }

        [Test]
        public void NonScenario_type_should_return_false_if_not_UnitScenario()
        {
            typeof(Dependency1)
                .IsUnitScenario()
                .ShouldBe(false);
        }

        [Test]
        public void StoryScenario_instance_should_return_true_if_StoryScenario()
        {
            new StubUserStoryScenario()
                .IsStoryScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_type_should_return_true_if_StoryScenario()
        {
            new StubUserStoryScenario()
                .IsStoryScenario()
                .ShouldBe(true);
        }

        [Test]
        public void StoryScenario_instance_should_return_false_if_not_StoryScenario()
        {
            new StubUnitScenario()
                .IsStoryScenario()
                .ShouldBe(false);;
        }

        [Test]
        public void StoryScenario_type_should_return_false_if__not_StoryScenario()
        {
            typeof (StubUnitScenario)
                .IsStoryScenario()
                .ShouldBe(false);
        }

        [Test]
        public void NonScenario_type_should_return_false_if_not_StoryScenario()
        {
            typeof(Dependency1)
                .IsStoryScenario()
                .ShouldBe(false);
        }

        [Test]
        public void Nested_StoryScenario_instance_should_return_true_if_StoryScenario()
        {
            new UserStoryScenarioWithAllSupportedStepsInRandomOrder()
                .IsStoryScenario()
                .ShouldBe(true); ;
        }

        [Test]
        public void Nested_StoryScenario_type_should_return_true_if_StoryScenario()
        {
            typeof(UserStoryScenarioWithAllSupportedStepsInRandomOrder)
                .IsStoryScenario()
                .ShouldBe(true); ;
        }

        [Test]
        public void SutType_should_return_type_of_SUT()
        {
            new StubUnitScenario()
                .SutType()
                .ShouldBe(typeof(ConcreteObjectWithMultipleConstructors));
        }
    }
}