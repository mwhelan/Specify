using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Specify.Providers
{
    internal class AutofacTestLifetimeScope : ITestLifetimeScope
    {
        private ILifetimeScope _container;
        private ContainerBuilder _containerBuilder;

        public AutofacTestLifetimeScope()
            : this(new ContainerBuilder())
        {
        }

        public AutofacTestLifetimeScope(ILifetimeScope container)
        {
            _container = container;
        }

        public AutofacTestLifetimeScope(ContainerBuilder containerBuilder)
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


        public T SystemUnderTest<T>() where T : class
        {
            if (!IsRegistered<T>())
            {
                Container.ComponentRegistry.Register(
                    RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
            }
            return Resolve<T>();
        }

        public void RegisterType<T>() where T : class
        {
            Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
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
            return Resolve<T>();
        }


        public bool IsRegistered<T>() where T : class
        {
            return IsRegistered(typeof(T));
        }

        public bool IsRegistered(System.Type type)
        {
            return Container.IsRegistered(type);
        }
    }
}
