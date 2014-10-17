using System;

namespace Specify.Containers
{
    public class SpecificationContext<TSut> : IDisposable where TSut : class
    {
        private TSut _systemUnderTest;
        public IDependencyResolver LifetimeScope { get; private set; }

        public SpecificationContext(IDependencyResolver lifetimeScope)
        {
            if(lifetimeScope == null)
                throw new ArgumentNullException("lifetimeScope");
            LifetimeScope = lifetimeScope;
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = LifetimeScope.Resolve<TSut>();
            }
            return _systemUnderTest;
        }

        public TService DependencyFor<TService>()
        {
            return LifetimeScope.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot inject dependencies after the System Under Test has been created");
            }
            LifetimeScope.Inject(instance);
        }

        public void Dispose()
        {
            LifetimeScope.Dispose();
        }
    }
}
