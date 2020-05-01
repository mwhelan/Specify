using System;
using System.Collections.Generic;
using DryIoc;
using Specify.Mocks;

namespace Specify.Containers
{
    /// <summary>
    /// Adapter for the DryIoc container with auto mocking using the specified mocking provider.
    /// </summary>
    public class DryMockingContainer : DryContainer
    {
        private readonly IMockFactory _mockFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DryMockingContainer"/> class.
        /// </summary>
        /// <param name="mockFactory">The mock factory.</param>
        /// <param name="container">The container.</param>
        public DryMockingContainer(IMockFactory mockFactory, DryIoc.IContainer container)
            : base(container)
        {
            _mockFactory = mockFactory;
        }

        /// <inheritdoc />
        public override bool CanResolve(Type type)
        {
            var canResolve = type.CanBeResolvedUsingContainer(x => x.IsInterface() || base.CanResolve(x), false);
            return canResolve;
        }

        /// <inheritdoc />
        public override object Get(Type serviceType, string key = null)
        {
            if (serviceType.IsEnumerable())
            {
                return GetMultiple(serviceType);
            }

            RegisterMocksIfNecessary(serviceType);

            return base.Get(serviceType, key);
        }

        private void RegisterMocksIfNecessary(Type serviceType)
        {
            if (serviceType.IsInterface())
            {
                if (!CanResolve(serviceType))
                {
                    RegisterMock(serviceType);
                }
            }

            if (serviceType.IsClass())
            {
                var constructor = serviceType.GreediestConstructor();

                if (constructor == null)
                {
                    return;
                }

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!base.CanResolve(parameterInfo.ParameterType))
                    {
                        if (parameterInfo.ParameterType.IsSealed())
                        {
                            RegisterMocksIfNecessary(parameterInfo.ParameterType);
                        }
                        else
                        {
                            RegisterMock(parameterInfo.ParameterType);
                        }
                    }
                }
            }
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
            Container.RegisterDelegate(serviceType, _ => mockInstance, ifAlreadyRegistered: IfAlreadyRegistered.Replace);
        }
    }
}
