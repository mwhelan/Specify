using System;
using System.Collections.Generic;
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

        /// <inheritdoc />
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

        /// <inheritdoc />
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
        public IEnumerable<object> GetMultiple(Type baseType)
        {
            return _sourceContainer.GetMultiple(baseType);
        }

        /// <inheritdoc />
        public IEnumerable<T> GetMultiple<T>() where T : class
        {
            return _sourceContainer.GetMultiple<T>();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _sourceContainer.Dispose();
        }

        internal IContainer SourceContainer => _sourceContainer;
    }
}