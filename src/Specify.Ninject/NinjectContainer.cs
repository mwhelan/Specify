using System;
using System.Linq;
using Ninject;

namespace Specify.Ninject
{
    public class NinjectContainer : IContainer
    {
        protected IKernel _container;

        public NinjectContainer(IKernel container)
        {
            _container = container;
        }

        public IKernel Container => _container;

        public void Set<T>() where T : class
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
        public void Set<TService, TImplementation>()
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
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            Container.GetBindings(typeof(T))
                .Where(b => key != null && b.Metadata.Name == key || string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => Container.RemoveBinding(b));

            if (key == null)
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectApplicationContainer.ScenarioLifetimeScopeTag)
                    .InSingletonScope()
                    .BindingConfiguration.IsImplicit = true;
            }
            else
            {
                Container.Bind<T>().ToConstant(valueToSet)
                    //.InNamedScope(NinjectApplicationContainer.ScenarioLifetimeScopeTag)
                    .InSingletonScope()
                    .Named(key)
                    .BindingConfiguration.IsImplicit = true;
            }
            return valueToSet;
        }

        public T Get<T>(string key = null) where T : class
        {
            return Container.Get<T>(m => key == null && m.Name == null || m.Name == key);
        }

        public object Get(Type serviceType, string key = null)
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
            return CanResolve(typeof(T));
        }

        public bool CanResolve(Type serviceType)
        {
            if (serviceType.IsClass)
            {
                var constructor = serviceType.GreediestConstructor();

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    var canResolve = (bool)Container.CanResolve(parameterInfo.ParameterType);
                    if (!canResolve)
                    {
                        return false;
                    }
                }
            }

            return (bool)Container.CanResolve(serviceType);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}