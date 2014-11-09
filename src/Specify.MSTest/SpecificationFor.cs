using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Specify.MSTest
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
