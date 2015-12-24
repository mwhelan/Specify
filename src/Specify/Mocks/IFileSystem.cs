using System;
using System.Reflection;

namespace Specify.Mocks
{
    public interface IFileSystem
    {
        bool IsAssemblyAvailable(string assemblyName);
        Assembly Load(string assemblyName);
        Type GetTypeFrom(Assembly assembly, string typeName);
    }
}