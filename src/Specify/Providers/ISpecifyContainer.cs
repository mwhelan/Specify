using System;

namespace Specify.Providers
{
    public interface ISpecifyContainer : IDisposable
    {
        ISpecification Resolve(Type type);
        ITestLifetimeScope CreateTestLifetimeScope();
    }
}