using System;

namespace Specify.Containers
{
    public interface IDependencyScope : IDisposable
    {
        TService Resolve<TService>();
        void Inject<TService>(TService instance);
    }
}