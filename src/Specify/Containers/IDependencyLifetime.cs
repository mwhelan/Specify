using System;

namespace Specify.Containers
{
    public interface IDependencyLifetime : IDisposable
    {
        TService Resolve<TService>();
        void Inject<TService>(TService instance);
    }
}