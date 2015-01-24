using Xunit;

namespace Specify.WithXunit
{
    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut> where TSut : class
    {
        [Fact]
        public override void ExecuteTest()
        {
            base.ExecuteTest();
        }
    }
}
