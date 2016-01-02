using System;
using Specify.Mocks;

namespace Specify.Configuration
{
    /// <inheritdoc />
    public class SpecifyConfigurationScanner : ConfigurationScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof (SpecifyBootstrapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyConfigurationScanner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public SpecifyConfigurationScanner(IFileSystem fileSystem)
            : base(fileSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyConfigurationScanner"/> class.
        /// </summary>
        public SpecifyConfigurationScanner() 
            : this(new FileSystem()) { }
    }
}