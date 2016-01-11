using System;
using Specify.LightInject;

namespace Specify
{
    /// <summary>
    /// Adapter for the LightInject container.
    /// </summary>
    public class LightContainer : IContainer
    {
        /// <summary>
        /// The LightInject container.
        /// </summary>
        internal readonly IServiceContainer Container;

        private readonly Scope _scope;

        /// <summary>
        /// Initializes a new instance of the <see cref="LightContainer"/> class.
        /// </summary>
        /// <param name="container">The target <see cref="IServiceContainer"/>.</param>
        /// <param name="scope">The wrapped <see cref="Scope"/>.</param>
        internal LightContainer(IServiceContainer container, Scope scope)
        {
            Container = container;
            _scope = scope;
        }

        /// <inheritdoc />
        public void Set<T>() where T : class
        {
            Container.Register<T>(new PerScopeLifetime());
        }

        /// <inheritdoc />
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            Container.Register<TService, TImplementation>(new PerScopeLifetime());
        }

        /// <inheritdoc />
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (key == null)
            {
                Container.RegisterInstance<T>(valueToSet);
            }
            else
            {
                Container.RegisterInstance<T>(valueToSet, key);
            }
            return valueToSet;
        }

        ///// <summary>
        ///// Gets all implementations of a type.
        ///// </summary>
        ///// <param name="baseType">The type that each implementation implements.</param>
        ///// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        //public virtual IEnumerable<object> GetMultiple(Type baseType)
        //{
        //    return Container.GetAllInstances(baseType);
        //}

        ///// <summary>
        ///// Gets all implementations of a type.
        ///// </summary>
        ///// <typeparam name="T">The type that each implementation implements.</typeparam>
        ///// <returns>IEnumerable&lt;TInterface&gt;.</returns>
        //public IEnumerable<T> GetMultiple<T>() where T : class
        //{
        //    return GetMultiple(typeof(T)).Cast<T>();
        //}

        /// <inheritdoc />
        public T Get<T>(string key = null) where T : class
        {
            return (T) Get(typeof (T), key);
        }

        /// <inheritdoc />
        public virtual object Get(Type serviceType, string key = null)
        {
            if (key == null)
            {
                return Container.GetInstance(serviceType);
            }
            else
            {
                return Container.GetInstance(serviceType, key);
            }
        }

        /// <inheritdoc />
        public bool CanResolve<T>() where T : class
        {
            return CanResolve(typeof(T));
        }

        /// <inheritdoc />
        public virtual bool CanResolve(Type serviceType)
        {
            if (serviceType.IsClass)
            {
                var constructor = serviceType.GreediestConstructor();

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!Container.CanGetInstance(parameterInfo.ParameterType, string.Empty))
                    {
                        return false;
                    }
                }
            }

            return Container.CanGetInstance(serviceType, string.Empty);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _scope?.Dispose();
        }
    }

    /// <summary>
    /// Class LightMockingContainer.
    /// </summary>
    public class LightMockingContainer : LightContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LightMockingContainer"/> class.
        /// </summary>
        /// <param name="container">The target <see cref="IServiceContainer" />.</param>
        /// <param name="scope">The wrapped <see cref="Scope" />.</param>
        internal LightMockingContainer(IServiceContainer container, Scope scope) 
            : base(container, scope) { }

        /// <inheritdoc />
        public override bool CanResolve(Type serviceType)
        {
            if (serviceType.IsClass)
            {
                return true;
            }

            return base.CanResolve(serviceType);
        }

        /// <inheritdoc />
        public override object Get(Type serviceType, string key = null)
        {
            if (serviceType.IsClass)
            {
                if (!Container.CanGetInstance(serviceType, string.Empty))
                {
                    Container.Register(serviceType);
                }
            }
            return base.Get(serviceType, key);
        }
    }
}