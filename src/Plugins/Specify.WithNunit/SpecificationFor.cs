using NUnit.Framework;

namespace Specify.WithNunit
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
