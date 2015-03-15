using System;

namespace Specify.Containers
{
    public class SutFactory<TSut> : IDisposable 
        where TSut : class
    {
        private readonly IContainer _container;
        private TSut _systemUnderTest;

        public SutFactory(IContainer container)
        {
            _container = container;
        }

        public TSut SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = _container.Get<TSut>();
                }
                return _systemUnderTest;
            }
            set { _systemUnderTest = value; }
        }

        public void RegisterType<T>() where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register type after SUT is created.");
            }
            _container.RegisterType<T>();
        }

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register instance after SUT is created.");
            }
            return _container.RegisterInstance(valueToSet, key);
        }

        public T Get<T>(string key = null) where T : class
        {
            return _container.Get<T>(key);
        }

        public object Get(Type serviceType, string key = null)
        {
            return _container.Get(serviceType, key);
        }

        public bool IsRegistered<T>() where T : class
        {
            return _container.IsRegistered<T>();
        }

        public bool IsRegistered(Type type)
        {
            return _container.IsRegistered(type);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        internal IContainer Container { get { return _container; } }
    }
}