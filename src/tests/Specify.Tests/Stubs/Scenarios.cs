using Shouldly;
using TestStack.BDDfy;

namespace Specify.Tests.Stubs
{
    class StubUnitScenario : ScenarioFor<ConcreteObjectWithMultipleConstructors> { }
    class StubUserStoryScenario : ScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }
    class StubValueStoryScenario : ScenarioFor<ConcreteObjectWithMultipleConstructors, TicTacToeValueStory> { }

    class ConcreteObjectWithNoConstructorUnitScenario : ScenarioFor<ConcreteObjectWithNoConstructor> { }

    [Story(
        Title = "Title from attribute",
        TitlePrefix = "Title prefix from attribute",
        AsA = "As a programmer",
        IWant = "I want to be able to explicitly specify a story",
        SoThat = "So that I can share a story definition between several scenarios")]
    class StubUserStoryScenarioForWithStoryAttribute : ScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }

    class StubUserStoryScenarioForWithExamples :
                ScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory>
    {
        public StubUserStoryScenarioForWithExamples()
        {
            Examples = new ExampleTable("First Example", "Second Example")
            {
                {1, "foo"},
                {2, "bar"}
            };
        }
        public string SecondExample { get; set; }

        public void GivenStepWith__FirstExample__PassedAsParameter(int firstExample)
        {
            firstExample.ShouldBeOneOf(1, 2);
        }

        public void GivenStepWith__SecondExample__AccessedViaProperty()
        {
            SecondExample.ShouldBeOneOf("foo", "bar");
        }
    }
}
