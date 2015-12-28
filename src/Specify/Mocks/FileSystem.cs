using System;
using System.IO;
using System.Reflection;

namespace Specify.Mocks
{
    /// <summary>
    /// Wrapper around file system and reflection methods.
    /// </summary>
    public class FileSystem : IFileSystem
    {
        /// <inheritdoc />
        public bool IsAssemblyAvailable(string assemblyName)
        {
            try
            {
                Assembly.Load(assemblyName);
                return true;
            }
            catch (FileNotFoundException)
            {
                return false;
            }
        }

        /// <inheritdoc />
        public Assembly Load(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        /// <inheritdoc />
        public Type GetTypeFrom(Assembly assembly, string typeName)
        {
            return assembly.GetType(typeName);
        }
    }
}