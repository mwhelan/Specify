using System;
using System.Reflection;

namespace Specify.Mocks
{
    public class MoqMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        public MoqMockFactory() 
            : this(new FileSystem()) { }

        public MoqMockFactory(IFileSystem fileSystem)
        {
            var assembly = fileSystem.Load("Moq");
            _mockOpenType = fileSystem.GetTypeFrom(assembly, "Moq.Mock`1");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type Moq.Mock`1 in assembly " + assembly.Location);
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