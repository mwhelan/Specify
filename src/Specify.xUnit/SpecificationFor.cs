using Xunit;

namespace Specify.xUnit
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