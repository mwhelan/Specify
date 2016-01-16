using System.Linq;
using Specify.Mocks;

namespace Specify.Configuration.Scanners
{
    /// <summary>
    /// Creates the appropriate ConfigScanner, depending on whether Specify if being used alone or with a plugin.
    /// </summary>
    public class ConfigScannerFactory
    {
        /// <summary>
        /// Chooses the configuration scanner. Specify itself, and each plugin, has its own configuration scanner.
        /// If Specify is being used alone its configuration scanner is used. If a plugin is installed its configuration scanner is used.
        /// </summary>
        /// <returns>IConfigScanner.</returns>
        public static IConfigScanner SelectScanner(IFileSystem fileSystem = null)
        {
            fileSystem = fileSystem ?? new FileSystem();
            var scanners = fileSystem
                .GetAllTypesFromAppDomain()
                .Where(type => type.IsConcreteTypeOf<IConfigScanner>())
                .ToList();

            return scanners.Count() == 1
                ? scanners.First().Create<IConfigScanner>()
                : scanners
                    .Except(new[] {typeof (SpecifyConfigScanner)})
                    .First()
                    .Create<IConfigScanner>();
        }
    }
}