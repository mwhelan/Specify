using NUnit.Framework;

namespace Specify.NUnit
{
    [TestFixture]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [Test]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}