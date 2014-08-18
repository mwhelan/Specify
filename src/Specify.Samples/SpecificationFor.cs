using NUnit.Framework;

namespace Specify.Samples
{
    public abstract class SpecificationFor<T> : Core.SpecificationFor<T>
    {
        [Test]
        public override void Run()
        {
            base.Run();
        }
    }
}
