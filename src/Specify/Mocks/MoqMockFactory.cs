using System;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the Moq mocking provider.
    /// </summary>
    public class MoqMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        /// <summary>
        /// Initializes a new instance of the <see cref="MoqMockFactory"/> class.
        /// </summary>
        public MoqMockFactory() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MoqMockFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <exception cref="System.InvalidOperationException">Unable to find Type Moq.Mock`1 in assembly  + assembly.Location</exception>
        public MoqMockFactory(IFileSystem fileSystem)
        {
            var assembly = fileSystem.Load("Moq");
            _mockOpenType = fileSystem.GetTypeFrom(assembly, "Moq.Mock`1");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type Moq.Mock`1 in assembly " + assembly.Location);
        }

        /// <inheritdoc />
        public object CreateMock(Type type)
        {
            Type closedType = _mockOpenType.MakeGenericType(new[] {type});
            PropertyInfo objectProperty = closedType.GetProperty("Object", type);
            object instance = Activator.CreateInstance(closedType);

            return objectProperty.GetValue(instance, null);
        }
    }
}