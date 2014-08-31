using System;

namespace Specify.Containers
{
    public interface ISutResolver<out TSut> : IDisposable where TSut : class
    {
        TService Resolve<TService>();
        void Inject<TService>(TService instance) where TService : class;
        TSut SystemUnderTest();

    }
}