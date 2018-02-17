using System;
using System.Collections.Generic;
using System.Linq;
using DryIoc;

namespace Specify.Microsoft.DependencyInjection
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
            Container.UseInstance(valueToSet, IfAlreadyRegistered.Replace);
            return valueToSet;
        }

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <param name="implementationTypes">Types that implement T.</param>
        public void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes)
        {
            //Container.reg.RegisterMultiple(baseType, implementationTypes);
            foreach (var type in implementationTypes)
            {
                Container.Register(type, ifAlreadyRegistered:IfAlreadyRegistered.Replace);
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
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        public virtual IEnumerable<object> GetMultiple(Type baseType)
        {
            return Container.ResolveMany(baseType);
        }

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        public IEnumerable<T> GetMultiple<T>() where T : class
        {
            return GetMultiple(typeof(T)).Cast<T>();
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
            // This is the preferred approach as it doesn't actually resolve the item from the container
            var isResolvable = Container.Resolve<Func<T>>(ifUnresolved: IfUnresolved.ReturnDefault) != null;
            return isResolvable;
        }

        /// <inheritdoc />
        public virtual bool CanResolve(Type type)
        {
            // TODO: This actually resolves the item from the container.
            // DryIoc does support checking without a full resolve but have to work out the non-generic syntax
            var isResolvable = Container.Resolve(type, ifUnresolved: IfUnresolved.ReturnDefault) != null;
            return isResolvable;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
