using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Specify.Containers
{
    public class IocContainer : IContainer
    {
        private ILifetimeScope _container;
        protected ContainerBuilder _containerBuilder;

        public IocContainer()
            : this(new ContainerBuilder())
        {
        }

        public IocContainer(ILifetimeScope container)
        {
            _container = container;
        }

        public IocContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        protected ILifetimeScope Container
        {
            get
            {
                if (_container == null)
                    _container = _containerBuilder.Build();
                return _container;
            }
        }

        public void Register<T>() where T : class
        {
            Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>()
                .InstancePerDependency()
                .CreateRegistration());
        }

        public void Register<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container
                .ComponentRegistry
                .Register(RegistrationBuilder.ForType<TImplementation>().As<TService>()
                .InstancePerLifetimeScope()
                .CreateRegistration());
        }

        public T Register<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .InstancePerLifetimeScope().CreateRegistration());

            }
            else
            {
                Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .As(new KeyedService(key, typeof(T)))
                        .InstancePerLifetimeScope().CreateRegistration());
            }
            return Resolve<T>(key);
        }

        public T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return Container.Resolve<T>();
            }
            else
            {
                return Container.ResolveKeyed<T>(key);
            }
        }

        public object Resolve(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.Resolve(serviceType);
            }
            else
            {
                return Container.ResolveKeyed(key, serviceType);
            }
        }

        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        public bool CanResolve(Type type)
        {
            return Container.IsRegistered(type);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
