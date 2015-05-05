using System;
using System.Linq;
using Ninject;

namespace Specify.Ninject
{
    public class NinjectContainer : IScenarioContainer
    {
        protected IKernel _container;

        public NinjectContainer(IKernel container)
        {
            this._container = container;
        }

        public IKernel Container
        {
            get
            {
                return this._container;
            }
        }

        public void Register<T>() where T : class
        {
            this.Container.GetBindings(typeof(T))
                .Where(b => string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => this.Container.RemoveBinding(b));

            this.Container.Bind<T>().To<T>()
                .InstancePerScenario()
                .BindingConfiguration.IsImplicit = true;
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            this.Container.GetBindings(typeof(TService))
                .Where(b => string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => this.Container.RemoveBinding(b));

            this.Container.Bind<TService>().To<TImplementation>()
                .InstancePerScenario()
                .BindingConfiguration.IsImplicit = true;
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            this.Container.GetBindings(typeof(T))
                .Where(b => key != null && b.Metadata.Name == key || string.IsNullOrEmpty(b.Metadata.Name))
                .ToList()
                .ForEach(b => this.Container.RemoveBinding(b));

            if (key == null)
            {
                this.Container.Bind<T>().ToConstant(valueToSet)
                    .InstancePerScenario()
                    .BindingConfiguration.IsImplicit = true;
            }
            else
            {
                this.Container.Bind<T>().ToConstant(valueToSet)
                    .InstancePerScenario()
                    .Named(key)
                    .BindingConfiguration.IsImplicit = true;
            }
            return valueToSet;
        }

        public T Resolve<T>(string key = null) where T : class
        {
            return this.Container.Get<T>(m => key == null && m.Name == null || m.Name == key);
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return this.Container.Get(serviceType);
            }
            else
            {
                return this.Container.Get(serviceType, key);
            }
        }

        public bool CanResolve<T>() where T : class
        {
            return this.Container.CanResolve<T>();
        }

        public bool CanResolve(Type type)
        {
            return this.Container.CanResolve(type) != null;
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }
    }
}
