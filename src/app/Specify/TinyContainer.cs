﻿using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoC;

namespace Specify
{
    /// <summary>
    /// Adapter for the TinyIoc container.
    /// </summary>
    public class TinyContainer : IContainer, IContainerRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TinyContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public TinyContainer(TinyIoCContainer container)
        {
            Container = container;
        }

        /// <summary>
        /// The TinyIoc container.
        /// </summary>
        public TinyIoCContainer Container { get; }

        public virtual IContainer GetChildContainer()
        {
            return new TinyContainer(Container.GetChildContainer());
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

        /// <summary>
        /// Gets all implementations of a type.
        /// </summary>
        /// <param name="baseType">The type that each implementation implements.</param>
        /// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        public virtual IEnumerable<object> GetMultiple(Type baseType)
        {
            return Container.ResolveAll(baseType, true);
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
        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        /// <inheritdoc />
        public virtual bool CanResolve(Type type)
        {
            return Container.CanResolve(type, ResolveOptions.FailUnregisteredAndNameNotFound);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}