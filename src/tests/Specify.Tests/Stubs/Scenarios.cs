using NSubstitute;
using Shouldly;
using Specify.Stories;
using TestStack.BDDfy;

namespace Specify.Tests.Stubs
{
    public abstract class TestableScenarioFor<TSut, TStory> : ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Specify.Stories.Story, new()
    {
        protected TestableScenarioFor()
        {
            Container = new ContainerFor<TSut>(Substitute.For<IContainer>());
        }
    }
    public abstract class TestableScenarioFor<TSut> : TestableScenarioFor<TSut, SpecificationStory>
        where TSut : class { }
    abstract class ParentScenario : ScenarioFor<object>
    {
        internal abstract class ChildScenario : ParentScenario
        {
            internal class GrandChildScenario : ChildScenario { }
        }
    }

    class StubUnitScenario : TestableScenarioFor<ConcreteObjectWithMultipleConstructors> { }
    class StubUserStoryScenario : TestableScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }
    class StubValueStoryScenario : TestableScenarioFor<ConcreteObjectWithMultipleConstructors, TicTacToeValueStory> { }

    class StubUnitScenarioWithCustomTitle : TestableScenarioFor<ConcreteObjectWithMultipleConstructors>
    {
        public StubUnitScenarioWithCustomTitle()
        {
            this.Story.Title = "Custom title";
        }
    }

    class ConcreteObjectWithNoConstructorUnitScenario : TestableScenarioFor<ConcreteObjectWithNoConstructor> { }

    [Story(
        Title = "Title from attribute",
        TitlePrefix = "Title prefix from attribute",
        AsA = "As a programmer",
        IWant = "I want to be able to explicitly specify a story",
        SoThat = "So that I can share a story definition between several scenarios",
        ImageUri = "http://www.google.co.uk",
        StoryUri = "http://www.bbc.co.uk")]
    class StubUserStoryScenarioForWithStoryAttribute : TestableScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory> { }

    class StubUserStoryScenarioForWithExamples :
        TestableScenarioFor<ConcreteObjectWithMultipleConstructors, WithdrawCashUserStory>, IScenario
    {
        public int ExamplesWasCalled;

        ExampleTable IScenario.Examples 
        {
            get
            {
                ExamplesWasCalled++;
                return base.Examples;
            }
            set { base.Examples = value; }
        }

        public StubUserStoryScenarioForWithExamples()
        {
            Container = new ContainerFor<ConcreteObjectWithMultipleConstructors>(Substitute.For<IContainer>());
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

    public class StubScenarioWithNumberOverridden : TestableScenarioFor<object>
    {
        public override int Number => 29;
    }
    public class StubScenarioWithNumberSetCtor : TestableScenarioFor<object>
    {
        public StubScenarioWithNumberSetCtor()
        {
            Number = 29;
        }
    }

}
