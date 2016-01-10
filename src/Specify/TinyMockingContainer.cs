using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Specify.Mocks;
using TinyIoC;

namespace Specify
{
    /// <summary>
    /// Adapter for the TinyIoc container with auto mocking using the specified mocking provider.
    /// </summary>
    public class TinyMockingContainer : TinyContainer
    {
        private readonly IMockFactory _mockFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="TinyMockingContainer"/> class.
        /// </summary>
        /// <param name="mockFactory">The mock factory.</param>
        /// <param name="container">The container.</param>
        public TinyMockingContainer(IMockFactory mockFactory, TinyIoCContainer container)
            : base(container)
        {
            _mockFactory = mockFactory;
        }

        /// <inheritdoc />
        public override bool CanResolve(Type type)
        {
            return true;
        }

        /// <inheritdoc />
        public override object Get(Type serviceType, string key = null)
        {
            if (serviceType.IsEnumerable())
            {
                return GetMultiple(serviceType);
            }
            if (serviceType.IsInterface)
            {
                if (!Container.CanResolve(serviceType))
                {
                    RegisterMock(serviceType);
                }
            }
            if (serviceType.IsClass)
            {
                var constructor = serviceType.GreediestConstructor();

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!Container.CanResolve(parameterInfo.ParameterType))
                    {
                        RegisterMock(parameterInfo.ParameterType);
                    }
                }
            }
            return base.Get(serviceType, key);
        }

        /// <inheritdoc />
        public override IEnumerable<object> GetMultiple(Type baseType)
        {
            if (!baseType.IsEnumerable())
            {
                throw new ArgumentException(
                    $"Only IEnumerable<T> types can be passed to the GetMultiple method.  {baseType.AssemblyQualifiedName} is invalid");
            }
            return base.GetMultiple(baseType);
        }

        private void RegisterMock(Type serviceType)
        {
            var mockInstance = _mockFactory.CreateMock(serviceType);
            Container.Register(serviceType, mockInstance);
        }
    }
}