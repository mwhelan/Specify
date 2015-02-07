using NUnit.Framework;

namespace Specify.Examples.Nunit
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
