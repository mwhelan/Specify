using NUnit.Framework;

namespace Specs.Unit.ApiTemplate
{
    [TestFixture]
    public abstract class TestsFor<TSut> : Specify.TestsFor<TSut> where TSut : class
    {
        [SetUp]
        public virtual void SetUp()
        {
            BaseSetup();
        }

        [TearDown]
        public virtual void TearDown()
        {
            BaseTearDown();
        }
    }
}
