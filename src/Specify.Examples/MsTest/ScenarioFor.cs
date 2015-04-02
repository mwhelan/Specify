using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specify.Stories;

namespace Specify.Examples.MsTest
{
    [TestClass]
    public abstract class ScenarioFor<TSut, TStory> : Specify.ScenarioFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [TestMethod]
        public override void Specify()
        {
            base.Specify();
        }
    }

    [TestClass]
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
        where TSut : class
    {
        [TestMethod]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
