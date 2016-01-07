using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Specify.Autofac
{
    public class AutofacContainer : Specify.IContainer
    {
        private ILifetimeScope _container;
        protected ContainerBuilder _containerBuilder;

        public AutofacContainer()
            : this(new ContainerBuilder())
        {
        }

        public AutofacContainer(ILifetimeScope container)
        {
            _container = container;
        }

        public AutofacContainer(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        protected ILifetimeScope Container
        {
            get
            {
                if (_container == null)
                {
                    _container = _containerBuilder.Build();
                }
                return _container;
            }
        }

        public void Set<T>() where T : class
        {
            Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>()
                .InstancePerLifetimeScope()
                .CreateRegistration());
        }

        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container
                .ComponentRegistry
                .Register(RegistrationBuilder.ForType<TImplementation>().As<TService>()
                .InstancePerLifetimeScope()
                .CreateRegistration());
        }

        public T Set<T>(T valueToSet, string key = null) where T : class
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
            return Get<T>(key);
        }

        public T Get<T>(string key = null) where T : class
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

        public object Get(Type serviceType, string key = null)
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

        public bool CanGet<T>() where T : class
        {
            return CanGet(typeof(T));
        }

        public bool CanGet(Type type)
        {
            return Container.IsRegistered(type);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
