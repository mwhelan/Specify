using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Specify.WithAutofacNSubstitute
{
    public class AutofacTestLifetimeScope : ITestLifetimeScope
    {
        private ILifetimeScope _container;
        private ContainerBuilder _containerBuilder;

        public AutofacTestLifetimeScope()
            : this(new ContainerBuilder())
        {
        }

        public AutofacTestLifetimeScope(ILifetimeScope container)
        {
            this._container = container;
        }

        public AutofacTestLifetimeScope(ContainerBuilder containerBuilder)
        {
            this._containerBuilder = containerBuilder;
        }

        protected ILifetimeScope Container
        {
            get
            {
                if (this._container == null)
                    this._container = this._containerBuilder.Build();
                return this._container;
            }
        }

        public void Dispose()
        {
            this.Container.Dispose();
        }


        public T SystemUnderTest<T>() where T : class
        {
            if (!this.IsRegistered<T>())
            {
                this.Container.ComponentRegistry.Register(
                    RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
            }
            return this.Resolve<T>();
        }

        public void RegisterType<T>() where T : class
        {
            this.Container.ComponentRegistry.Register(RegistrationBuilder.ForType<T>().InstancePerLifetimeScope().CreateRegistration());
        }

        public T Resolve<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return this.Container.Resolve<T>();
            }
            else
            {
                return this.Container.ResolveKeyed<T>(key);
            }
        }

        public T RegisterInstance<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                this.Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .InstancePerLifetimeScope().CreateRegistration());

            }
            else
            {
                this.Container.ComponentRegistry
                    .Register(RegistrationBuilder.ForDelegate((c, p) => valueToSet)
                        .As(new KeyedService(key, typeof(T)))
                        .InstancePerLifetimeScope().CreateRegistration());
            }
            return this.Resolve<T>();
        }


        public bool IsRegistered<T>() where T : class
        {
            return this.IsRegistered(typeof(T));
        }

        public bool IsRegistered(System.Type type)
        {
            return this.Container.IsRegistered(type);
        }
    }
}
