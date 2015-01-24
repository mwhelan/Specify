using Xunit;

namespace Specify.WithXunit
{
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [Fact]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : UserStory
    {
        [Fact]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

}