using System;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using IContainer = Specify.Containers.IContainer;

namespace Specify.Examples.Autofac
{
    public class AutofacContainer : Containers.IContainer
    {
        private ILifetimeScope _container;
        private ContainerBuilder _containerBuilder;

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
                    _container = _containerBuilder.Build();
                return _container;
            }
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        public void RegisterType<T>() where T : class
        {
            Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
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

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
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
            return Get<T>();
        }

        public bool IsRegistered<T>() where T : class
        {
            return IsRegistered(typeof(T));
        }

        public bool IsRegistered(Type type)
        {
            return Container.IsRegistered(type);
        }

        public IContainer CreateChildContainer()
        {
            return new AutofacContainer(Container.BeginLifetimeScope());
        }
    }
}
