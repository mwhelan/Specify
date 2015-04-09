using System;

using Ninject;

using Specify.Containers;

namespace Specify.Examples.Ninject
{
    public class NinjectContainer : IContainer
    {
        protected IKernel _container;

        public NinjectContainer(IKernel container)
        {
            _container = container;
        }

        protected IKernel Container
        {
            get
            {
                return _container;
            }
        }

        public void Register<T>() where T : class
        {
            if (this.CanResolve<T>())
            {
                Container.Rebind<T>().To<T>()
                    .InTransientScope();
            }
            else
            {
                Container.Bind<T>().To<T>()
                    .InTransientScope();
            }
        }

        // This needs to be lifetime scope per scenario
        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (this.CanResolve<TService>())
            {
                Container.Rebind<TService>().To<TImplementation>()
                    .InSingletonScope();
            }
            else
            {
                Container.Bind<TService>().To<TImplementation>()
                    //.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag)
                    .InSingletonScope();
            }
        }

        // This needs to be lifetime scope per scenario
        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag)
                    .InSingletonScope();
            }
            else
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag)
                    .InSingletonScope()
                    .Named(key);
            }
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return Container.GetDefault<T>();
            }
            else
            {
                return Container.GetNamedOrDefault<T>(key);
            }
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.GetDefault(serviceType);
            }
            else
            {
                return Container.GetNamedOrDefault(serviceType, key);
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
