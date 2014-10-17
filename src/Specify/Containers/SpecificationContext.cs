using System;

namespace Specify.Containers
{
    public class SpecificationContext<TSut> : IDisposable where TSut : class
    {
        private TSut _systemUnderTest;
        public IDependencyResolver Resolver { get; private set; }

        public SpecificationContext(IDependencyResolver resolver)
        {
            if(resolver == null)
                throw new ArgumentNullException("resolver");
            Resolver = resolver;
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = Resolver.Resolve<TSut>();
            }
            return _systemUnderTest;
        }

        public TService DependencyFor<TService>()
        {
            return Resolver.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot inject dependencies after the System Under Test has been created");
            }
            Resolver.Inject(instance);
        }

        public void Dispose()
        {
            Resolver.Dispose();
        }
    }
}
