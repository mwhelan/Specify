using System;
using System.Reflection;
using Moq;
using Specify.Mocks;

namespace Specify.Examples.Mocks
{
    public class MoqMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public MoqMockFactory()
        {
            _mockOpenType = typeof (Mock<>);
        }

        public object CreateMock(Type type)
        {
            Type closedType = _mockOpenType.MakeGenericType(new[] { type });
            PropertyInfo objectProperty = closedType.GetProperty("Object", type);
            object instance = Activator.CreateInstance(closedType);
            return objectProperty.GetValue(instance, null);
        }

        public bool IsAvailable()
        {
            return true;
        }
    }
}