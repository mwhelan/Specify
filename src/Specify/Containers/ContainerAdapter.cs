using System;

namespace Specify.Containers
{
    public class ContainerAdapter : IContainer
    {
        private IContainer _container;

        public ContainerAdapter()
        {
            _container = new DefaultContainer();    
        }

        public void SetContainer(IContainer container)
        {
            _container = container;
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        public void RegisterType<T>() where T : class
        {
            _container.RegisterType<T>();
        }

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
        {
            return _container.RegisterInstance(valueToSet, key);
        }

        public T Resolve<T>(string key = null) where T : class
        {
            return _container.Resolve<T>(key);
        }

        public object Resolve(Type serviceType, string key = null)
        {
            return _container.Resolve(serviceType, key);
        }

        public bool IsRegistered<T>() where T : class
        {
            return _container.IsRegistered<T>();
        }

        public bool IsRegistered(Type type)
        {
            return _container.IsRegistered(type);
        }

        public IContainer CreateChildContainer()
        {
            return _container.CreateChildContainer();
        }
    }
}
