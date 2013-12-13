using Xunit;

namespace Specify.Samples
{
    public abstract class SpecificationFor<T> : Core.SpecificationFor<T>
    {
        [Fact]
        public override void Run()
        {
            base.Run();
        }
    }
}
