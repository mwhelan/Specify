using System;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Wrapper around the file system.
    /// </summary>
    public interface IFileSystem
    {
        /// <summary>
        /// Determines whether an assembly is loaded with the specified name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns><c>true</c> if [is assembly available] [the specified assembly name]; otherwise, <c>false</c>.</returns>
        bool IsAssemblyAvailable(string assemblyName);

        /// <summary>
        /// Loads the specified assembly name.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>Assembly.</returns>
        Assembly Load(string assemblyName);

        /// <summary>
        /// Gets the type of the specified name from the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeName">Name of the type.</param>
        /// <returns>Type.</returns>
        Type GetTypeFrom(Assembly assembly, string typeName);
    }
}