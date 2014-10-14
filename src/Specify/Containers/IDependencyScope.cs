using System;

namespace Specify.Containers
{
    public interface IDependencyScope : IDisposable
    {
        TService Get<TService>();
        void Set<TService>(TService instance) where TService : class;
    }
}