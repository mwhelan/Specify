using System.IO;
using System.Reflection;

namespace Specify.Configuration.Mocking
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
    }
}