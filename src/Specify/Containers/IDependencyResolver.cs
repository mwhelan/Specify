using System;

namespace Specify.Containers
{
    public interface IDependencyResolver : IDisposable
    {
        TService Resolve<TService>();
        object Resolve(Type type);
        void Inject<TService>(TService instance) where TService : class;
    }
}