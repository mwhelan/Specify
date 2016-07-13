using System;

namespace Specify.Mocks
{
    public abstract class MockFactoryBase : IMockFactory
    {
        private readonly IFileSystem _fileSystem;
        private Type _mockOpenType;

        /// <inheritdoc />
        public string MockProviderName => GetType().Name.Replace("MockFactory", string.Empty);

        /// <inheritdoc />
        public abstract object CreateMock(Type type);

        /// <inheritdoc />
        public bool IsProviderAvailable => _fileSystem.IsAssemblyAvailable(MockProviderName);

        /// <summary>
        /// The name of the type in the mock library that generates mocks.
        /// </summary>
        protected abstract string MockTypeName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="IMockFactory"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        protected MockFactoryBase(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// The open generic type that creates mock objects.
        /// </summary>
        protected Type MockOpenType
        {
            get
            {
                if (_mockOpenType == null)
                {
                    var assembly = _fileSystem.Load(MockProviderName);
                    _mockOpenType = _fileSystem.GetTypeFrom(assembly, MockTypeName);
                    if (_mockOpenType == null)
                        throw new InvalidOperationException($"Unable to find Type {MockTypeName} in assembly {assembly.Location}");
                }
                return _mockOpenType;
            }
        }
    }
}