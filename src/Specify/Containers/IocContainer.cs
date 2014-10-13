using System;

namespace Specify.Containers
{
    public class IocContainer<TSut> : ITestContainer<TSut> where TSut : class
    {
        private TSut _systemUnderTest;
        private readonly IDependencyResolver _container;

        public IocContainer(IDependencyResolver container)
        {
            _container = container;
        }

        public TService DependencyFor<TService>()
        {
            return _container.Resolve<TService>();
        }

        public void InjectDependency<TService>(TService instance) where TService : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot inject dependencies after the System Under Test has been created");
            }
            _container.Inject<TService>(instance);
        }

        public TSut SystemUnderTest()
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = _container.Resolve<TSut>();
            }
            return _systemUnderTest;
        }
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}