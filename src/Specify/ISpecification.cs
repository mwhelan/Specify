using System;

namespace Specify
{
    public interface ISpecification
    {
        void ExecuteTest();
        //T DependencyFor<T>();
        //void InjectDependency<TService>(TService instance) where TService : class;
        Type Story { get; }
        string Title { get; }
    }
}