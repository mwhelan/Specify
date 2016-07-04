using System;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the NSubstitute mocking provider.
    /// </summary>
    public class NSubstituteMockFactory : IMockFactory
    {
        private readonly Type _mockOpenType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NSubstituteMockFactory"/> class.
        /// </summary>
        public NSubstituteMockFactory() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="NSubstituteMockFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system wrapper.</param>
        /// <exception cref="System.InvalidOperationException">Unable to find Type NSubstitute.Substitute in assembly  + assembly.Location</exception>
        public NSubstituteMockFactory(IFileSystem fileSystem)
        {
            var assembly = fileSystem.Load("NSubstitute");
            _mockOpenType = fileSystem.GetTypeFrom(assembly, "NSubstitute.Substitute");
            if (_mockOpenType == null)
                throw new InvalidOperationException("Unable to find Type NSubstitute.Substitute in assembly " + assembly.Location);
        }

        /// <inheritdoc />
        public object CreateMock(Type type)
        {
            var methods = _mockOpenType.GetMethods();
            foreach (var method in methods)
            {
                if (IsGenericForMethod(method))
                {
                    var closedGenericForMethod = method.MakeGenericMethod(type);
                    return closedGenericForMethod.Invoke(null, new object[1] { null });
                }
            }
            return null;
        }

        private static bool IsGenericForMethod(MethodInfo method)
        {
            return method.ContainsGenericParameters && method.GetGenericArguments().Length == 1;
        }
    }
}
