using Xunit;

namespace Specify.xUnit
{
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [Fact]
        public override ISpecification ExecuteTest()
        {
            return base.ExecuteTest();
        }
    }
}
