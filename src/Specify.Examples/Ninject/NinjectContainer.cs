using System;
using Ninject;
using Specify.Containers;

namespace Specify.Examples.Ninject
{
    public class NinjectContainer : IContainer
    {
        private IKernel _container;
        //protected ContainerBuilder _containerBuilder;

        //public NinjectContainer()
        //    : this(new ContainerBuilder())
        //{
        //}

        public NinjectContainer(IKernel container)
        {
            _container = container;
        }

        //public NinjectContainer(ContainerBuilder containerBuilder)
        //{
        //    _containerBuilder = containerBuilder;
        //}

        protected IKernel Container
        {
            get
            {
                //if (_container == null)
                //    _container = _containerBuilder.Build();
                return _container;
            }
        }

        public void Register<T>() where T : class
        {
            string name = typeof(T).Name;
            Container.Bind<T>().To<T>().InTransientScope().Named(name);
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            string name = typeof(TService).Name;
            Container.Bind<TService>().To<TImplementation>().Named(name);
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                string name = typeof (T).Name;
                Container.Bind<T>().ToConstant(valueToSet).Named(name);
            }
            else
            {
                Container.Bind<T>().ToConstant(valueToSet).Named(key);
            }
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                string name = typeof(T).Name;
                return Container.Get<T>(name);
            }
            else
            {
                return Container.Get<T>(key);
            }
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                string name = serviceType.Name;
                return Container.Get(serviceType, name);
            }
            else
            {
                return Container.Get(serviceType, key);
            }
        }

        public bool CanResolve<T>() where T : class
        {
            return Container.CanResolve<T>();
        }

        public bool CanResolve(Type type)
        {
            return Container.CanResolve(type) != null;
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
