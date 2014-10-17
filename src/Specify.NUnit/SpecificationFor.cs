using NUnit.Framework;

namespace Specify.NUnit
{
    [TestFixture]
    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut> where TSut : class
    {
        [Test]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}
