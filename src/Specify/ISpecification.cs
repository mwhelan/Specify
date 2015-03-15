using System;
using Specify.Containers;

namespace Specify
{
    public interface ISpecification
    {
        SutFactory Container { get; }
        void ExecuteTest();
        Type Story { get; }
        string Title { get; }
        int Number { get; set; }
    }
}