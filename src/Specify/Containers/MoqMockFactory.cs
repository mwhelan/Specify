using System;
using System.Reflection;

namespace Specify.Containers
{
    public class MoqMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public MoqMockFactory()
        {
            var moqAssembly = Assembly.Load("Moq");
            _mockOpenType = moqAssembly.GetType("Moq.Mock`1");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type Moq.Mock<T> in assembly " + moqAssembly.Location);
        }

        public object CreateMock(Type type)
        {
            Type closedType = _mockOpenType.MakeGenericType(new[] {type});
            PropertyInfo objectProperty = closedType.GetProperty("Object", type);
            object instance = Activator.CreateInstance(closedType);

            return objectProperty.GetValue(instance, null);
        }
    }
}