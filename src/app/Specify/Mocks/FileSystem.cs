using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Specify.lib;

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
                Assembly.Load(new AssemblyName(assemblyName));
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
            return Assembly.Load(new AssemblyName(assemblyName));
        }

        /// <inheritdoc />
        public Type GetTypeFrom(Assembly assembly, string typeName)
        {
            return assembly.GetType(typeName);
        }

        /// <inheritdoc />
        public IEnumerable<Type> GetAllTypesFromAppDomain()
        {
            return AssemblyTypeResolver.GetAllTypesFromAppDomain();
        }
    }
}