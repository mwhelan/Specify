using System;
using System.Collections.Generic;
using System.Linq;
using TinyIoC;

namespace Specify
{
    /// <summary>
    /// Adapter for the TinyIoc container.
    /// </summary>
    public class TinyContainer : IContainer
    {
        /// <summary>
        /// The TinyIoc container.
        /// </summary>
        public readonly TinyIoCContainer Container;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyContainer"/> class.
        /// </summary>
        public TinyContainer()
        {
            Container = TinyIoCContainer.Current;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyContainer"/> class.
        /// </summary>
        /// <param name="container">The container.</param>
        public TinyContainer(TinyIoCContainer container)
        {
            Container = container;
        }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            Container.Register<T>().AsSingleton();
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>();
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.Register<T>(valueToSet);
            }
            else
            {
                Container.Register<T>(valueToSet, key);
            }
            return valueToSet;
        }

        /// <inheritdoc />
        public virtual T Get<T>(string key = null) where T : class
        {
            if (key == null)
            {
                return Container.Resolve<T>();
            }
            else
            {
                return Container.Resolve<T>(key);
            }
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
        public bool CanGet<T>() where T : class
        {
            return Container.CanResolve<T>();
        }

        /// <inheritdoc />
        public bool CanGet(Type type)
        {
            return Container.CanResolve(type);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
