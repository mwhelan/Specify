using System;
using System.Linq;
using Ninject;

namespace Specify.Examples.Ninject
{
    public class NinjectContainer : IScenarioContainer
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
            Container.GetBindings(typeof(T))
                .Where(b => string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => Container.RemoveBinding(b));

            Container.Bind<T>().To<T>()
                .InSingletonScope()
                .BindingConfiguration.IsImplicit = true;
        }

        // This needs to be lifetime scope per scenario
        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.GetBindings(typeof(TService))
                .Where(b => string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => Container.RemoveBinding(b));

            Container.Bind<TService>().To<TImplementation>()
                .InSingletonScope()
                .BindingConfiguration.IsImplicit = true;
        }

        // This needs to be lifetime scope per scenario
        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            Container.GetBindings(typeof(T))
                .Where(b => key != null && b.Metadata.Name == key || string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => Container.RemoveBinding(b));

            if (key == null)
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag)
                    .InSingletonScope()
                    .BindingConfiguration.IsImplicit = true;
            }
            else
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectDependencyResolver.ScenarioLifetimeScopeTag)
                    .InSingletonScope()
                    .Named(key)
                    .BindingConfiguration.IsImplicit = true;
            }
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            return Container.Get<T>(m => key == null && m.Name == null || m.Name == key);
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.Get(serviceType);
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
