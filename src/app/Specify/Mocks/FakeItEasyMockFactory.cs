using System;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the FakeItEasy mocking provider.
    /// </summary>
    public class FakeItEasyMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeItEasyMockFactory"/> class.
        /// </summary>
        public FakeItEasyMockFactory() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeItEasyMockFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <exception cref="System.InvalidOperationException">Unable to find Type FakeItEasy.A in assembly  + assembly.Location</exception>
        public FakeItEasyMockFactory(IFileSystem fileSystem)
        {
            var assembly = fileSystem.Load("FakeItEasy");
            _mockOpenType = fileSystem.GetTypeFrom(assembly, "FakeItEasy.A");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type FakeItEasy.A in assembly " + assembly.Location);
        }

        /// <inheritdoc />
        public object CreateMock(Type type)
        {
            var openFakeMethod = _mockOpenType.GetMethodInfo("Fake", Type.EmptyTypes);
            var closedFakeMethod = openFakeMethod.MakeGenericMethod(type);

            try
            {
                return closedFakeMethod.Invoke(null, null);
            }
            catch
            {
                return null;
            }
        }
    }
}