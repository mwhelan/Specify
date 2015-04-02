using Specify.Stories;
using Xunit;

namespace Specify.Examples.Xunit
{
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [Fact]
        public override void Specify()
        {
            base.Specify();
        }
    }

    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        [Fact]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
