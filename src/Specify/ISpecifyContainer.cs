namespace Specify
{
    using System;

    public interface ISpecifyContainer : IDisposable
    {
        ISpecification Resolve(Type type);
        ITestLifetimeScope CreateTestLifetimeScope();
    }
}