using System;

namespace Specify
{
    /// <summary>
    /// Represents a container that provides the SUT and its dependencies to specifications. The container might be
    /// a full IoC container or an auto mocking container.
    /// </summary>
    public interface IContainer : IDisposable
    {
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
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        T Get<T>(string key = null) where T : class;

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        object Get(Type serviceType, string key = null);

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><c>true</c> if this instance can resolve; otherwise, <c>false</c>.</returns>
        bool CanGet<T>() where T : class;

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can resolve the specified type; otherwise, <c>false</c>.</returns>
        bool CanGet(Type type);
    }
}
