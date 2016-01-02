using System;
using System.Linq;
using Specify.Mocks;

namespace Specify.Configuration
{
    /// <inheritdoc />
    public abstract class ConfigurationScanner : IConfigurationScanner
    {
        private readonly IFileSystem _fileSystem;
        /// <summary>
        /// Gets the default type of the bootstrapper.
        /// </summary>
        /// <value>The default type of the bootstrapper.</value>
        protected abstract Type DefaultBootstrapperType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationScanner"/> class.
        /// </summary>
        protected ConfigurationScanner() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationScanner"/> class.
        /// </summary>
        protected ConfigurationScanner(IFileSystem fileSystem)
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

        /// <summary>
        /// Chooses the configuration scanner. Each Specify assembly has one configuration scanner.
        /// </summary>
        /// <returns>IConfigurationScanner.</returns>
        public static IConfigurationScanner FindScanner(IFileSystem fileSystem = null)
        {
            fileSystem = fileSystem ?? new FileSystem();
            var scanners = fileSystem
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsConcreteTypeOf<IConfigurationScanner>())
                .ToList();

            return scanners.Count() == 1
                ? scanners.First().Create<IConfigurationScanner>()
                : scanners
                    .Except(new[] {typeof (SpecifyConfigurationScanner)})
                    .First()
                    .Create<IConfigurationScanner>();
        }
    }
}