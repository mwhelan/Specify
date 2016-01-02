using System;
using System.Linq;
using Specify.Configuration;
using Specify.Mocks;

namespace Specify.Autofac
{
    /// <inheritdoc />
    public class SpecifyAutofacConfigurationScanner : ConfigurationScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof(SpecifyAutofacBootstrapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyAutofacConfigurationScanner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public SpecifyAutofacConfigurationScanner(IFileSystem fileSystem)
            : base(fileSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyAutofacConfigurationScanner"/> class.
        /// </summary>
        public SpecifyAutofacConfigurationScanner() 
            : this(new FileSystem()) { }
    }
}
