using System;

namespace Specify.Containers
{
    public interface ITestContainer : IDisposable
    {
        IDependencyResolver CreateTestLifetimeScope();
    }
}