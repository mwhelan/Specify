using NUnit.Framework;

namespace Specify.IntegrationTests.App
{
    [TestFixture]
    public abstract class NScenarioFor<T> : ScenarioFor<T> where T : class
    {
        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
