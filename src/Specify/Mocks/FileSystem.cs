using System;
using System.IO;
using System.Reflection;

namespace Specify.Mocks
{
    public class FileSystem : IFileSystem{
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

        public Assembly Load(string assemblyName)
        {
            return Assembly.Load(assemblyName);
        }

        public Type GetTypeFrom(Assembly assembly, string typeName)
        {
            return assembly.GetType(typeName);
        }
    }
}