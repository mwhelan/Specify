using System;

namespace Specify.Containers
{
    public class SpecificationContext<TSut> : IDisposable 
        where TSut : class
    {
        private readonly IContainer _sourceContainer;
        private TSut _systemUnderTest;

        public SpecificationContext(IContainer sourceContainer)
        {
            _sourceContainer = sourceContainer;
        }

        internal TSut SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = _sourceContainer.Get<TSut>();
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
            _sourceContainer.RegisterType<T>();
        }

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InvalidOperationException("Cannot register instance after SUT is created.");
            }
            return _sourceContainer.RegisterInstance(valueToSet, key);
        }

        public T Get<T>(string key = null) where T : class
        {
            return _sourceContainer.Get<T>(key);
        }

        public object Get(Type serviceType, string key = null)
        {
            return _sourceContainer.Get(serviceType, key);
        }

        public bool IsRegistered<T>() where T : class
        {
            return _sourceContainer.IsRegistered<T>();
        }

        public bool IsRegistered(Type type)
        {
            return _sourceContainer.IsRegistered(type);
        }

        public void Dispose()
        {
            _sourceContainer.Dispose();
        }

        internal IContainer SourceContainer { get { return _sourceContainer; } }
    }
}