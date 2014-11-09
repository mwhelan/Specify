using System;

namespace Specify
{
    public interface ISpecification
    {
        void ExecuteTest();
        Type Story { get; }
        string Title { get; }
    }
}