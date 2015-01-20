using System;

namespace Specify
{
    public interface ISpecification
    {
        ITestLifetimeScope Container { get; set; }
        void ExecuteTest();
        Type Story { get; }
        string Title { get; }
    }
}