using Microsoft.VisualStudio.TestTools.UnitTesting;
using Specify.Stories;

namespace Specify.Examples.MsTest
{
    [TestClass]
    public abstract class SpecificationFor<TSut, TStory> : Specify.SpecificationFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [TestMethod]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }

    [TestClass]
    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut>
        where TSut : class
    {
        [TestMethod]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}
