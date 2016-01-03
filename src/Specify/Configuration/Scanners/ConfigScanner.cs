using System;
using System.Linq;
using Specify.Mocks;

namespace Specify.Configuration.Scanners
{
    /// <inheritdoc />
    public abstract class ConfigScanner : IConfigScanner
    {
        private readonly IFileSystem _fileSystem;
        /// <summary>
        /// Gets the default type of the bootstrapper.
        /// </summary>
        /// <value>The default type of the bootstrapper.</value>
        protected abstract Type DefaultBootstrapperType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigScanner"/> class.
        /// </summary>
        protected ConfigScanner() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigScanner"/> class.
        /// </summary>
        protected ConfigScanner(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <inheritdoc />
        public IConfigureSpecify GetConfiguration()
        {
            var bootstrapper = _fileSystem
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(IsSpecifyConfigurationImplementation());
            var config = bootstrapper != null
                ? bootstrapper.Create<IConfigureSpecify>()
                : DefaultBootstrapperType.Create<IConfigureSpecify>();

            return config;
        }

        private Func<Type, bool> IsSpecifyConfigurationImplementation()
        {
            return type => type.IsConcreteTypeOf<IConfigureSpecify>()
                           && type != DefaultBootstrapperType;
        }
    }
}