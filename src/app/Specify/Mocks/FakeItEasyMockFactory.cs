using System;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the FakeItEasy mocking provider.
    /// </summary>
    public class FakeItEasyMockFactory : MockFactoryBase
    {
        /// <inheritdoc />
        public FakeItEasyMockFactory() 
            : base(new FileSystem()) { }

        /// <inheritdoc />
        public FakeItEasyMockFactory(IFileSystem fileSystem) 
            : base(fileSystem) { }

        /// <inheritdoc />
        protected override string MockTypeName => "FakeItEasy.A";

        /// <inheritdoc />
        public override object CreateMock(Type type)
        {
            var openFakeMethod = MockOpenType.GetMethodInfo("Fake", Type.EmptyTypes);
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