using Xunit;

namespace Specify.xUnit
{
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [Fact]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}
