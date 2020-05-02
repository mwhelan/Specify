using System;
using System.Collections.Generic;
using DryIoc;

namespace Specify.Containers
{
    /// <summary>
    /// Adapter for the DryIoc container.
    /// </summary>
    public class DryContainer : IContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DryContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public DryContainer(DryIoc.IContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// The DryIoc container.
        /// </summary>
        public DryIoc.IContainer Container { get; }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            Container.Register<T>(Reuse.Singleton, ifAlreadyRegistered:IfAlreadyRegistered.Replace);
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(Reuse.Singleton, ifAlreadyRegistered:IfAlreadyRegistered.Replace);
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            Container.RegisterDelegate(_ => valueToSet, ifAlreadyRegistered: IfAlreadyRegistered.Replace,
                serviceKey:key);
            return valueToSet;
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <param name="serviceType">The type that each implementation implements.</param>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple(Type serviceType, IEnumerable<Type> implementationTypes)
        {
            foreach (var implementationType in implementationTypes)
            {
                Container.Register(serviceType, implementationType);
            }
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple<T>(IEnumerable<Type> implementationTypes)
        {
            SetMultiple(typeof(T), implementationTypes);
        }

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <param name="serviceType">The type that each implementation implements.</param>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        public virtual IEnumerable<object> GetMultiple(Type serviceType)
        {
            return Container.ResolveMany(serviceType);
        }

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        public IEnumerable<T> GetMultiple<T>() where T : class
        {
            return Container.ResolveMany<T>();
        }

        /// <inheritdoc />
        public T Get<T>(string key = null) where T : class
        {
            return (T)Get(typeof(T), key);
        }

        /// <inheritdoc />
        public virtual object Get(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.Resolve(serviceType);
            }
            else
            {
                return Container.Resolve(serviceType, key);
            }
        }

        /// <inheritdoc />
        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        /// <inheritdoc />
        public virtual bool CanResolve(Type type)
        {
            // This is the preferred approach as it doesn't actually resolve the item from the container
            var isResolvable = Container.Resolve<Func<object>>(requiredServiceType: type, ifUnresolved: IfUnresolved.ReturnDefault) != null;
            return isResolvable;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
