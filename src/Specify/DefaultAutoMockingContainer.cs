using System;
using System.Linq;
using System.Reflection;
using Specify.Mocks;
using TinyIoC;

namespace Specify
{

    public class DefaultAutoMockingContainer : DefaultScenarioContainer
    {
        private readonly IMockFactory _mockFactory;

        public DefaultAutoMockingContainer(IMockFactory mockFactory)
            : base(new TinyIoCContainer())
        {
            _mockFactory = mockFactory;
        }

        public override T Resolve<T>(string key = null)
        {
            return (T)Resolve(typeof(T), key);
        }

        public override object Resolve(Type serviceType, string key = null)
        {
            if (serviceType.IsInterface)
            {
                if (!CanResolve(serviceType))
                {
                    RegisterMock(serviceType);
                }
            }
            if (serviceType.IsClass)
            {
                var constructor = GreediestConstructor(serviceType);

                foreach (var parameterInfo in constructor.GetParameters())
                {
                    if (!CanResolve(parameterInfo.ParameterType))
                    {
                        RegisterMock(parameterInfo.ParameterType);
                    }
                }
            }
            return base.Resolve(serviceType, key);
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