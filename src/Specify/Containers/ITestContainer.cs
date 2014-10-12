using System;

namespace Specify.Containers
{
    public interface ITestContainer<out TSut> : IDisposable where TSut : class
    {
        TSut SystemUnderTest();
        TService DependencyFor<TService>();
        void InjectDependency<TService>(TService instance) where TService : class;
    }
}