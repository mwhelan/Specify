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

        public void RegisterType<T>() where T : class
        {
            _container.Register<T>();
        }

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                _container.Register<T>(valueToSet);
            }
            else
            {
                _container.Register<T>(valueToSet, key);
            }
            return valueToSet;
        }

        public T Get<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return _container.Resolve<T>();
            }
            else
            {
                return _container.Resolve<T>(key);
            }
        }

        public object Get(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return _container.Resolve(serviceType);
            }
            else
            {
                return _container.Resolve(serviceType, key);
            }
        }

        public bool IsRegistered<T>() where T : class
        {
            return _container.CanResolve<T>();
        }

        public bool IsRegistered(Type type)
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
