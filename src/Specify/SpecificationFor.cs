using Specify.Containers;

namespace Specify
{
    public abstract class SpecificationFor<TSut> 
        : Specification<TSut, AutoMockingContainer<TSut>> where TSut : class
    {
    }
}