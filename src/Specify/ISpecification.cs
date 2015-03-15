using System;
using Specify.Containers;
using TestStack.BDDfy;

namespace Specify
{
    public interface ISpecification
    {
        void Specify();
        ExampleTable Examples { get; set; }
        Type Story { get; }
        string Title { get; }
        int Number { get; set; }
        void SetContainer(IContainer container);
    }
}