using System;

namespace Specify.Mocks
{
    /// <summary>
    /// Adapter for the Moq mocking provider.
    /// </summary>
    public class MoqMockFactory : MockFactoryBase
    {
        /// <inheritdoc />
        public MoqMockFactory() 
            : base(new FileSystem()) { }

        /// <inheritdoc />
        public MoqMockFactory(IFileSystem fileSystem) 
            : base(fileSystem) { }

        /// <inheritdoc />
        protected override string MockTypeName => "Moq.Mock`1";

        /// <inheritdoc />
        public override object CreateMock(Type type)
        {
            var closedType = MockOpenType.MakeGenericType(new[] {type});
            var objectProperty = closedType.GetPropertyInfo("Object", type);
            var instance = closedType.Create();

            return objectProperty.GetValue(instance, null);
        }
    }
}