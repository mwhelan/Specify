using System;
using System.Collections.Generic;

namespace Specify
{
    /// <summary>
    /// Represents a container that provides the SUT and its dependencies to specifications. The container might be
    /// a full IoC container or an auto mocking container.
    /// </summary>
    public interface IContainer : IDisposable
    {
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
        /// Determines whether an instance of this type can be resolved from container. 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><c>true</c> if this type is registered with the container; otherwise, <c>false</c>.</returns>
        bool CanResolve<T>() where T : class;

        /// <summary>
        /// Determines whether an instance of this type can be resolved from container. 
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this type is registered with the container; otherwise, <c>false</c>.</returns>
        bool CanResolve(Type type);

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        IEnumerable<object> GetMultiple(Type baseType);

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <typeparam name="T">The type that each implementation implements.</typeparam>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        IEnumerable<T> GetMultiple<T>() where T : class;
    }
}