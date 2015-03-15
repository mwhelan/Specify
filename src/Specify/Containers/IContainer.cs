using System;

namespace Specify.Containers
{
    /// <summary>
    /// Represents a container that provides the SUT and its dependencies to specifications. The container might be
    /// a full IoC container or an automocking container.
    /// </summary>
    public interface IContainer : IDisposable
    {
        /// <summary>
        /// Registers a type to the container. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void RegisterType<T>() where T : class;

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T RegisterInstance<T>(T valueToSet, string key = null) where T : class;

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        T Get<T>(string key = null) where T : class;

        object Get(Type serviceType, string key = null);
 

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool IsRegistered<T>() where T : class;

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        bool IsRegistered(Type type);

        IContainer CreateChildContainer();
    }
}
