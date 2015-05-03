using System;

namespace Specify
{
    /// <summary>
    /// Represents a container that provides the SUT and its dependencies to specifications. The container might be
    /// a full IoC container or an automocking container.
    /// </summary>
    public interface IScenarioContainer : IDisposable
    {
        /// <summary>
        /// Registers a type to the container. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>() where T : class;

       /// <summary>
       /// Registers an implementation type for a service interface
       /// </summary>
       /// <typeparam name="TService">The interface type</typeparam>
       /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        void Register<TService, TImplementation>() 
            where TService : class 
            where TImplementation : class, TService;

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Resolve{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Register<T>(T valueToSet, string key = null) where T : class;

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Resolve<T>(string key = null) where T : class;

        object Resolve(Type serviceType, string key = null);
 

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool CanResolve<T>() where T : class;

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        bool CanResolve(Type type);
    }
}
