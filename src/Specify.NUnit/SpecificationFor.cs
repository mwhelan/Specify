using NUnit.Framework;

namespace Specify.NUnit
{
    [TestFixture]
    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut> where TSut : class
    {
        [Test]
        public override ISpecification ExecuteTest()
        {
            return base.ExecuteTest();
        }
    }
}
