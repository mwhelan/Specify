using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;
using Specify.Containers;

namespace Specify.Autofac
{
    /// <summary>
    /// Adapter for the Autofac container.
    /// </summary>
    public class AutofacChildContainerBuilder : IChildContainerBuilder
    {
        private ILifetimeScope _parentScope;

        public List<Action<ContainerBuilder>> BuilderActions { get; } = new List<Action<ContainerBuilder>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AutofacContainer"/> class.
        /// </summary>
        /// <param name="container">An <see cref="ILifetimeScope"/> tracks the instantiation of component instances.</param>
        public AutofacChildContainerBuilder(ILifetimeScope container)
        {
            _parentScope = container;
        }

        public IContainer GetChildContainer()
        {
            var childContainer = _parentScope.BeginLifetimeScope(builder =>
            {
                foreach (var action in BuilderActions)
                {
                    action(builder);
                }
            });
                
            return new AutofacContainer(childContainer);
        }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            BuilderActions.Add(builder => builder.RegisterType<T>().InstancePerLifetimeScope());
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            BuilderActions.Add(builder => builder
                .RegisterType<TImplementation>()
                .As<TService>()
                .InstancePerLifetimeScope());
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                BuilderActions.Add(builder => builder
                    .Register((c, p) => valueToSet)
                    .As<T>()
                    .InstancePerLifetimeScope());
            }
            else
            {
                BuilderActions.Add(builder => builder
                    .Register((c, p) => valueToSet)
                    .As(new KeyedService(key, typeof(T)))
                    .InstancePerLifetimeScope());
            }

            return valueToSet;
        }

        /// <inheritdoc />
        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {
                BuilderActions.Add(builder => builder
                    .RegisterType(type)
                    .As(baseType)
                    .InstancePerLifetimeScope());
            }
        }

        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            SetMultiple(typeof(T), implementationTypes);
        }
    }
}