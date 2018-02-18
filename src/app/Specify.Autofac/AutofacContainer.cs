using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Activators.Reflection;

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

        public ILifetimeScope Container
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

        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        /// <summary>
        /// Determines whether this instance can resolve the specified service type.
        /// The Autofac IsRegistered method can return true if a class is registered but still throw a DependencyResolutionException
        /// when that class is Resolved if a dependency of that class is not registered. By contrast, TinyIoc actually checks that the
        /// class can be resolved successfully. This behaviour is applied to Autofac to ensure consistency of behaviour across containers.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns><c>true</c> if this instance can resolve the specified service type; otherwise, <c>false</c>.</returns>
        public bool CanResolve(Type serviceType)
        {
            return serviceType.CanBeResolvedUsingContainer(x => Container.IsRegistered(x));
        }

        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {
                Container
                    .ComponentRegistry
                    .Register(RegistrationBuilder.ForType(type).As(baseType)
                        .InstancePerLifetimeScope()
                        .CreateRegistration());
            }
        }

        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            SetMultiple(typeof(T), implementationTypes);
        }

        public IEnumerable<object> GetMultiple(Type baseType)
        {
            return Container.ComponentRegistry
                .RegistrationsFor(new TypedService(baseType))
                .Select(x => x.Activator);
        }

        public IEnumerable<T> GetMultiple<T>() where T : class
        {
            return Container.Resolve<IEnumerable<T>>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
