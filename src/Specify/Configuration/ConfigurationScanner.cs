using System;
using System.Linq;
using Specify.Mocks;

namespace Specify.Configuration
{
    /// <summary>
    /// Initializes the Specify configuration.
    /// </summary>
    public class ConfigurationScanner
    {
        private readonly IFileSystem _fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationScanner"/> class.
        /// </summary>
        public ConfigurationScanner() 
            : this(new FileSystem()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationScanner"/> class.
        /// </summary>
        public ConfigurationScanner(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Scans the test assembly to find the appropriate bootstrapper class.
        /// </summary>
        /// <returns>IConfigureSpecify.</returns>
        public IConfigureSpecify GetConfiguration()
        {
            var bootstrapper = _fileSystem
                .GetAllTypesFromAppDomain()
                .FirstOrDefault(IsSpecifyConfigurationImplementation());
            var config = bootstrapper != null
                ? (IConfigureSpecify)Activator.CreateInstance(bootstrapper)
                : new SpecifyBootstrapper();

            return config;
        }

        private static Func<Type, bool> IsSpecifyConfigurationImplementation()
        {
            return type => typeof(IConfigureSpecify).IsAssignableFrom(type) 
            && type.IsClass && 
            !type.IsAbstract
            && type != typeof(SpecifyBootstrapper);
        }
    }
}