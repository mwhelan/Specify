using System;
using Specify.LightInject;

namespace Specify
{
    public class DefaultContainer : IScenarioContainer
    {
        private readonly IServiceContainer _container;

        public DefaultContainer()
        {
            _container = new ServiceContainer();
        }

        internal DefaultContainer(IServiceContainer container)
        {
            _container = container;
        }

        public void Register<T>() where T : class
        {
            _container.Register<T>(new PerContainerLifetime());
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>(new PerContainerLifetime());
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                _container.RegisterInstance(valueToSet);
            }
            else
            {
                _container.RegisterInstance(valueToSet, key);
            }
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return _container.GetInstance<T>();
            }
            else
            {
                return _container.GetInstance<T>(key);
            }
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return _container.GetInstance(serviceType);
            }
            else
            {
                return _container.GetInstance(serviceType, key);
            }
        }

        public bool CanResolve<T>() where T : class
        {
            return _container.CanGetInstance(typeof(T), string.Empty);
        }

        public bool CanResolve(Type type)
        {
            return _container.CanGetInstance(type, string.Empty);
        }

        public IScenarioContainer CreateChildContainer()
        {
            _container.BeginScope();
            return new DefaultContainer(_container);
        }

        public void Dispose()
        {
            _container.EndCurrentScope();
        }
    }
}