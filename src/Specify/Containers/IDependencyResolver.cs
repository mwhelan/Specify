using System;

namespace Specify.Containers
{
    public interface IDependencyResolver : IDisposable
    {
        IDependencyScope CreateScope();
    }
}