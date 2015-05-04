using Xunit;

namespace ContosoUniversity.UnitTests
{
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
