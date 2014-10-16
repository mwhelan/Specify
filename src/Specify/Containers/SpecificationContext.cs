using System;

namespace Specify.Containers
{
    public class SpecificationContext<TSut> : IDisposable where TSut : class
    {
        private TSut _systemUnderTest;
        public IDependencyScope Scope { get; private set; }

        public SpecificationContext(IDependencyScope scope)
        {
            Scope = scope;
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = Scope.Resolve<TSut>();
            }
            return _systemUnderTest;
        }

        public TService DependencyFor<TService>()
        {
            return Scope.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot inject dependencies after the System Under Test has been created");
            }
            Scope.Inject(instance);
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }
}
