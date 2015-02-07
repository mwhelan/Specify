using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Specify.Examples.MsTest
{
    [TestClass]
    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut> where TSut : class
    {
        [TestMethod]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}
