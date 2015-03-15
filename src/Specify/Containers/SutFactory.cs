using System;

namespace Specify.Containers
{
    public class SutFactory : IDisposable
    {
        private readonly IContainer _container;
        private object _systemUnderTest;

        public SutFactory(IContainer container)
        {
            _container = container;
        }

        public T SystemUnderTest<T>() where T : class
        {
            if (_systemUnderTest == null)
            {
                _systemUnderTest = _container.Resolve<T>();
            }
            return (T) _systemUnderTest;
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

        public T Resolve<T>(string key = null) where T : class
        {
            return _container.Resolve<T>(key);
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

        public IContainer Container { get { return _container; } }

        public void SetSystemUnderTest<T>(T value)
        {
            throw new NotImplementedException();
        }
    }
}