using NUnit.Framework;

namespace Specify.Examples.UnitSpecs
{
    public abstract class ScenarioFor<TSut> : Specify.ScenarioFor<TSut>
            where TSut : class
    {
        [Test]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
