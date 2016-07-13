using System;
using Specify.Exceptions;

namespace Specify
{
    /// <summary>
    /// Wrapper for various IContainer implementations that provides container for ScenarioFor/TestsFor classes and ensures consistent behaviour.
    /// </summary>
    /// <typeparam name="TSut">The type of the t sut.</typeparam>
    public class ContainerFor<TSut> : IContainer 
        where TSut : class
    {
        private readonly IContainer _sourceContainer;
        private TSut _systemUnderTest;

        /// <summary>
        /// Initializes a new instance of the <see cref="ContainerFor{TSut}"/> class.
        /// </summary>
        /// <param name="sourceContainer">The source container.</param>
        public ContainerFor(IContainer sourceContainer)
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
        /// <exception cref="InterfaceRegistrationException"></exception>
        public void Set<T>() where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            _sourceContainer.Set<T>();
        }

        /// <summary>
        /// Registers an implementation type for a service interface
        /// </summary>
        /// <typeparam name="TService">The interface type</typeparam>
        /// <typeparam name="TImplementation">The type that implements the service interface</typeparam>
        /// <exception cref="InterfaceRegistrationException"></exception>
        public void Set<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(TImplementation));
            }
            _sourceContainer.Set<TService, TImplementation>();
        }

        /// <summary>
        /// Sets a value in the container, so that from now on, it will be returned when you call <see cref="Get{T}" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valueToSet">The value to set.</param>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        /// <exception cref="InterfaceRegistrationException"></exception>
        public T Set<T>(T valueToSet, string key = null) where T : class
        {
            if (_systemUnderTest != null)
            {
                throw new InterfaceRegistrationException(typeof(T));
            }
            
            return _sourceContainer.Set(valueToSet, key);
        }

        /// <summary>
        /// Gets a value of the specified type from the container, optionally registered under a key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns>T.</returns>
        /// <exception cref="InterfaceResolutionException"></exception>
        public T Get<T>(string key = null) where T : class
        {
            try
            {
                return _sourceContainer.Get<T>(key);
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
        /// <exception cref="InterfaceResolutionException"></exception>
        public object Get(Type serviceType, string key = null)
        {
            try
            {
                return _sourceContainer.Get(serviceType, key);
            }
            catch (Exception ex)
            {
                throw new InterfaceResolutionException(ex, serviceType);
            }
        }

        /// <inheritdoc />
        public bool CanResolve<T>() where T : class
        {
            return _sourceContainer.CanResolve<T>();
        }

        /// <inheritdoc />
        public bool CanResolve(Type type)
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