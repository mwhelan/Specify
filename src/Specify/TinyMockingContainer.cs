using System;
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
        public override T Get<T>(string key = null)
        {
            return (T)Get(typeof(T), key);
        }

        /// <inheritdoc />
        public override object Get(Type serviceType, string key = null)
        {
            if (serviceType.IsInterface)
            {
                if (!CanGet(serviceType))
                {
                    RegisterMock(serviceType);
                }
            }
            if (serviceType.IsClass)
            {
                var constructor = GreediestConstructor(serviceType);

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!CanGet(parameterInfo.ParameterType))
                    {
                        RegisterMock(parameterInfo.ParameterType);
                    }
                }
            }
            return base.Get(serviceType, key);
        }

        private void RegisterMock(Type serviceType)
        {
            var mockInstance = _mockFactory.CreateMock(serviceType);
            Container.Register(serviceType, mockInstance);
        }

        private static ConstructorInfo GreediestConstructor(Type type)
        {
            return type.GetConstructors()
                .OrderByDescending(x => x.GetParameters().Length)
                .First();
        }
    }
}