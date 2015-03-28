using System;
using TinyIoC;

namespace Specify.Containers
{
    public class DefaultContainer : IContainer
    {
        private readonly TinyIoCContainer _container;

        public DefaultContainer()
        {
            _container = new TinyIoCContainer();
        }

        internal DefaultContainer(TinyIoCContainer container)
        {
            _container = container;
        }

        public void Register<T>() where T : class
        {
            _container.Register<T>();
        }

        public void Register<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService
        {
            _container.Register<TService, TImplementation>();
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            _container.Register<T>(valueToSet, key);
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            return _container.Resolve<T>(key);
        }

        public object Resolve(Type serviceType, string key = null)
        {
            return _container.Resolve(serviceType, key);
        }

        public bool CanResolve<T>() where T : class
        {
            return _container.CanResolve<T>();
        }

        public bool CanResolve(Type type)
        {
            return _container.CanResolve(type);
        }

        public IContainer CreateChildContainer()
        {
            return new DefaultContainer(_container.GetChildContainer());
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
