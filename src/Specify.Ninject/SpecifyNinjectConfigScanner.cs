using System;
using Specify.Configuration.Scanners;
using Specify.Mocks;

namespace Specify.Ninject
{
    /// <inheritdoc />
    public class SpecifyNinjectConfigScanner : ConfigScanner
    {
        /// <inheritdoc />
        protected override Type DefaultBootstrapperType => typeof(SpecifyNinjectBootstrapper);

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyNinjectConfigScanner"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public SpecifyNinjectConfigScanner(IFileSystem fileSystem)
            : base(fileSystem)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyNinjectConfigScanner"/> class.
        /// </summary>
        public SpecifyNinjectConfigScanner()
            : this(new FileSystem())
        { }
    }
}