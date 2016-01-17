using System;
using Specify.Mocks;

namespace Specify.Configuration.Scanners
{
    /// <inheritdoc />
    public class SpecifyConfigScanner : ConfigScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof (DefaultBootstrapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyConfigScanner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public SpecifyConfigScanner(IFileSystem fileSystem)
            : base(fileSystem) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyConfigScanner"/> class.
        /// </summary>
        public SpecifyConfigScanner() 
            : this(new FileSystem()) { }
    }
}