using System;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the NSubstitute mocking provider.
    /// </summary>
    public class NSubstituteMockFactory : MockFactoryBase
    {
        /// <inheritdoc />
        public NSubstituteMockFactory() 
            : base(new FileSystem()) { }

        /// <inheritdoc />
        public NSubstituteMockFactory(IFileSystem fileSystem) 
            : base(fileSystem) { }

        /// <inheritdoc />
        protected override string MockTypeName => "NSubstitute.Substitute";

        /// <inheritdoc />
        public override object CreateMock(Type type)
        {
            var methods = MockOpenType.GetMethods();
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
