using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Specify.Autofac
{
    /// <summary>
    /// Adapter for the Autofac container.
    /// </summary>
    public class AutofacContainer : Specify.IContainer
    {
        private ILifetimeScope _parentScope;
        private ILifetimeScope _childScope;
        protected ContainerBuilder ContainerBuilder;

        private List<Action<ContainerBuilder>> _builderActions = new List<Action<ContainerBuilder>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainer"/> class.
        /// </summary>
        public AutofacContainer()
            : this(new ContainerBuilder())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainer"/> class.
        /// </summary>
        /// <param name="container">An <see cref="ILifetimeScope"/> tracks the instantiation of component instances.</param>
        public AutofacContainer(ILifetimeScope container)
        {
            _parentScope = container;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainer"/> class.
        /// </summary>
        /// <param name="containerBuilder">Used to build an <see cref="IContainer"/> from component registrations.</param>
        public AutofacContainer(ContainerBuilder containerBuilder)
        {
            ContainerBuilder = containerBuilder;
            _parentScope = ContainerBuilder.Build();
        }

        /// <summary>
        /// The Autofac container.
        /// </summary>
        public ILifetimeScope Container => _childScope ?? _parentScope;

        public void BeginScope()
        {
            if (_childScope != null)
            {
                throw new ApplicationException("Cannot set scope more than once.");
            }

            _childScope = Container.BeginLifetimeScope(builder =>
            {
                foreach (var action in _builderActions)
                {
                    action(builder);
                }
            });
        }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            _builderActions.Add(builder => builder.RegisterType<T>().InstancePerLifetimeScope());
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            _builderActions.Add(builder => builder
                .RegisterType<TImplementation>()
                .As<TService>()
                .InstancePerLifetimeScope());
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                _builderActions.Add(builder => builder
                    .Register((c, p) => valueToSet)
                    .As<T>()
                    .InstancePerLifetimeScope());
            }
            else
            {
                _builderActions.Add(builder => builder
                    .Register((c, p) => valueToSet)
                    .As(new KeyedService(key, typeof(T)))
                    .InstancePerLifetimeScope());
            }

            return valueToSet;
        }

        /// <inheritdoc />
        /// <inheritdoc />
        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {
                _builderActions.Add(builder => builder
                    .RegisterType(type)
                    .As(baseType)
                    .InstancePerLifetimeScope());
            }
        }

        /// <inheritdoc />
        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            SetMultiple(typeof(T), implementationTypes);
        }

        public T Get<T>(string key = null) where T : class
        {
            return key == null ? Container.Resolve<T>() : Container.ResolveKeyed<T>(key);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        /// <inheritdoc />
        public IEnumerable<object> GetMultiple(Type baseType)
        {
            var enumerableOfType = typeof(IEnumerable<>).MakeGenericType(baseType);
            return (IEnumerable<object>)Container.ResolveService(new TypedService(enumerableOfType));
        }

        /// <inheritdoc />
        public IEnumerable<T> GetMultiple<T>() where T : class
        {
            return Container.Resolve<IEnumerable<T>>();
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

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}