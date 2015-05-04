using System;
using TinyIoC;

namespace Specify
{
    public class DefaultContainer : IScenarioContainer
    {
        protected readonly TinyIoCContainer Container;

        public DefaultContainer()
        {
            Container = new TinyIoCContainer();
        }

        internal DefaultContainer(TinyIoCContainer container)
        {
            Container = container;
        }

        public void Register<T>() where T : class
        {
            Container.Register<T>().AsSingleton();
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>();
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.Register<T>(valueToSet);
            }
            else
            {
                Container.Register<T>(valueToSet, key);
            }
            return valueToSet;
        }

        public virtual T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return Container.Resolve<T>();
            }
            else
            {
                return Container.Resolve<T>(key);
            }
        }

        public virtual object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.Resolve(serviceType);
            }
            else
            {
                return Container.Resolve(serviceType, key);
            }
        }

        public bool CanResolve<T>() where T : class
        {
            return Container.CanResolve<T>();
        }

        public bool CanResolve(Type type)
        {
            return Container.CanResolve(type);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
