using System;
using Specify.Containers;

namespace Specify
{
    public interface ISpecification
    {
        void Specify();
        Type Story { get; }
        string Title { get; }
        int Number { get; set; }
        void SetContainer(IContainer container);
    }
}