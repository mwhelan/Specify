using NUnit.Framework;

namespace Specify.Tests
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