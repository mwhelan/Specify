using System;
using Specify.Exceptions;

namespace Specify
{
    /// <summary>
    /// Wrapper for various IContainer implementations that provides container for Scenario classes and ensures consistent behaviour.
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    public class SutFactory<TSut> : IDisposable 
        where TSut : class
    {
        private readonly IContainer _sourceContainer;
        private TSut _systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="SutFactory{TSut}"/> class.
        /// </summary>
        /// <param name="sourceContainer">The source container.</param>
        public SutFactory(IContainer sourceContainer)
        {
            _sourceContainer = sourceContainer;
        }

        /// <summary>
        /// Gets or sets the System Under Test.
        /// </summary>
        /// <value>The system under test.</value>
        public TSut SystemUnderTest
        {
            get
            {
                if (_systemUnderTest == null)
                {

                    _systemUnderTest = Get<TSut>();
                }
                return _systemUnderTest;
            }
            set { _systemUnderTest = value; }
        }

        /// <summary>
        /// Registers a type to the container. 
        /// </summary>
        /// <typeparam name="T">The type of the component implementation.</typeparam>
        public void Set<T>() where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            _sourceContainer.Register<T>();
        }

        /// <summary>
        /// Registers an implementation type for a service interface
        /// </summary>
        /// <typeparam name="TService">The interface type</typeparam>
        /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(TImplementation));
            }
            _sourceContainer.Register<TService, TImplementation>();
        }

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            
            return _sourceContainer.Register(valueToSet, key);
        }

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        public T Get<T>(string key = null) where T : class
        {
            try
            {
                return _sourceContainer.Resolve<T>(key);
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, typeof(T));
            }
        }

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <param name="key">The key.</param>
        /// <returns>System.Object.</returns>
        public object Get(Type serviceType, string key = null)
        {
            try
            {
                return _sourceContainer.Resolve(serviceType, key);
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, serviceType);
            }
        }

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns><c>true</c> if this instance can resolve; otherwise, <c>false</c>.</returns>
        public bool IsRegistered<T>() where T : class
        {
            return _sourceContainer.CanResolve<T>();
        }

        /// <summary>
        /// Determines whether an instance of this type is registered.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if this instance can resolve the specified type; otherwise, <c>false</c>.</returns>
        public bool IsRegistered(Type type)
        {
            return _sourceContainer.CanResolve(type);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _sourceContainer.Dispose();
        }

        internal IContainer SourceContainer => _sourceContainer;
    }
}