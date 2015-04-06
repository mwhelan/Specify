namespace Specify.Configuration.Mocking
{
    public interface IFileSystem
    {
        bool IsAssemblyAvailable(string assemblyName);
    }
}