using TestStack.BDDfy;

namespace Specify.Tests.Stubs
{
    class StubUnitScenario : ScenarioFor<ConcreteObjectWithMultipleConstructors> { }
    class StubUserStoryScenario : ScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }

    class ConcreteObjectWithNoConstructorUnitScenario : ScenarioFor<ConcreteObjectWithNoConstructor> { }

    [Story(
        Title = "Title from attribute",
        TitlePrefix = "Title prefix from attribute",
        AsA = "As a programmer",
        IWant = "I want to be able to explicitly specify a story",
        SoThat = "So that I can share a story definition between several scenarios")]
    class StubUserStoryScenarioForWithStoryAttribute : ScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }
}
