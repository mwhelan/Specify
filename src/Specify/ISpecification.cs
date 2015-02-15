using System;
using Specify.Containers;

namespace Specify
{
    public interface ISpecification
    {
        SutFactory Container { get; set; }
        void ExecuteTest();
        Type Story { get; }
        string Title { get; }
    }
}