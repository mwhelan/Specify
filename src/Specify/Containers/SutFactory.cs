using System;

namespace Specify.Containers
{
    public class SutFactory<TSut> : IDisposable 
        where TSut : class
    {
        private readonly IContainer _sourceContainer;
        private TSut _systemUnderTest;

        public SutFactory(IContainer sourceContainer)
        {
            _sourceContainer = sourceContainer;
        }

        internal TSut SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = _sourceContainer.Resolve<TSut>();
                }
                return _systemUnderTest;
            }
            set { _systemUnderTest = value; }
        }

        public void Register<T>() where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register type after SUT is created.");
            }
            _sourceContainer.Register<T>();
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register service after SUT is created.");
            }
            _sourceContainer.Register<TService, TImplementation>();
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register instance after SUT is created.");
            }
            return _sourceContainer.Register(valueToSet, key);
        }

        public T Get<T>(string key = null) where T : class
        {
            return _sourceContainer.Resolve<T>(key);
        }

        public object Get(Type serviceType, string key = null)
        {
            return _sourceContainer.Resolve(serviceType, key);
        }

        public bool IsRegistered<T>() where T : class
        {
            return _sourceContainer.CanResolve<T>();
        }

        public bool IsRegistered(Type type)
        {
            return _sourceContainer.CanResolve(type);
        }

        public void Dispose()
        {
            _sourceContainer.Dispose();
        }

        internal IContainer SourceContainer { get { return _sourceContainer; } }
    }
}