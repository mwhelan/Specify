using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Specify.Examples.MsTest
{
    [TestClass]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut> where TSut : class
    {
        [TestMethod]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

    [TestClass]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : UserStory
    {
        [TestMethod]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}