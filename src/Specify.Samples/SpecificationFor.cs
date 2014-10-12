using NUnit.Framework;

namespace Specify.Samples
{
    public abstract class SpecificationFor<T> : Specify.SpecificationFor<T> where T : class
    {
        [Test]
        public override ISpecification ExecuteTest()
        {
            return base.ExecuteTest();
        }
    }
}
