using System;
using Specify.Exceptions;

namespace Specify
{
    public class SutFactory<TSut> : IDisposable 
        where TSut : class
    {
        private readonly IScenarioContainer _sourceContainer;
        private TSut _systemUnderTest;

        public SutFactory(IScenarioContainer sourceContainer)
        {
            _sourceContainer = sourceContainer;
        }

        internal TSut SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {
                    _systemUnderTest = Get<TSut>();
                }
                return _systemUnderTest;
            }
            set { _systemUnderTest = value; }
        }

        public void Register<T>() where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            _sourceContainer.Register<T>();
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(TImplementation));
            }
            _sourceContainer.Register<TService, TImplementation>();
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            
            return _sourceContainer.Register(valueToSet, key);
        }

        public T Get<T>(string key = null) where T : class
        {
            try
            {
                return _sourceContainer.Resolve<T>(key);
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, typeof(T));
            }
        }

        public object Get(Type serviceType, string key = null)
        {
            try
            {
                return _sourceContainer.Resolve(serviceType, key);
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, serviceType);
            }
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

        internal IScenarioContainer SourceContainer { get { return _sourceContainer; } }
    }
}