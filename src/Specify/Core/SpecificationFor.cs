using Specify.Containers;
using TestStack.BDDfy;

namespace Specify.Core
{
    public abstract class SpecificationFor<TSut> : Specification<TSut, AutoSubResolver<TSut>> where TSut : class
    {
        protected SpecificationFor()
            : base(new AutoSubResolver<TSut>()) { }
    }
}
