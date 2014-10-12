using System;

namespace Specify
{
    public interface ISpecification : IDisposable
    {
        ISpecification ExecuteTest();
        T DependencyFor<T>();
        void InjectDependency<TService>(TService instance) where TService : class;
        Type Story { get; }
        string Title { get; }
    }
}