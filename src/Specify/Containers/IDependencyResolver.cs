using System;

namespace Specify.Containers
{
    public interface IDependencyResolver : IDisposable
    {
        TService Resolve<TService>();
        void Inject<TService>(TService instance);
    }
}