using System;
using Specify.Configuration.Scanners;
using Specify.Mocks;

namespace Specify.Autofac
{
    /// <inheritdoc />
    public class SpecifyAutofacConfigScanner : ConfigScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof(DefaultAutofacBootstrapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyAutofacConfigScanner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public SpecifyAutofacConfigScanner(IFileSystem fileSystem)
            : base(fileSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyAutofacConfigScanner"/> class.
        /// </summary>
        public SpecifyAutofacConfigScanner() 
            : this(new FileSystem()) { }
    }
}
