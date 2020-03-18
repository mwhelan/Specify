using System;
using System.Collections.Generic;

namespace Specify.Containers
{
    public interface IChildContainerBuilder
    {
        IContainer GetChildContainer();

        /// <summary>
        /// Registers a type to the container.
        /// </summary>
        /// <typeparam name="T">The type of the component implementation.</typeparam>
        void Set<T>() where T : class;

        /// <summary>
        /// Registers an implementation type for a service interface
        /// </summary>
        /// <typeparam name="TService">The interface type</typeparam>
        /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        T Set<T>(T valueToSet, string key = null) where T : class;

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <param name="implementationTypes">Types that implement T.</param>
        void SetMultiple(Type baseType, IEnumerable<Type> implementationTypes);

        /// <summary>
        /// Register multiple implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <param name="implementationTypes">Types that implement T.</param>
        void SetMultiple<T>(IEnumerable<Type> implementationTypes);
    }
}