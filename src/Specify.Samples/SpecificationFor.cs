using NUnit.Framework;

namespace Specify.Samples
{
    public abstract class SpecificationFor<T> : Core.SpecificationFor<T> where T : class
    {
        [Test]
        public override void Run()
        {
            base.Run();
        }
    }
}
