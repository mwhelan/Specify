using Specify.Stories;
using Xunit;

namespace Specify.Examples.Xunit
{
    public abstract class SpecificationFor<TSut, TStory> : Specify.SpecificationFor<TSut, TStory>
        where TSut : class
        where TStory : Story, new()
    {
        [Fact]
        public override void Specify()
        {
            base.Specify();
        }
    }

    public abstract class SpecificationFor<TSut> : Specify.SpecificationFor<TSut>
        where TSut : class
    {
        [Fact]
        public override void Specify()
        {
            base.Specify();
        }
    }
}
