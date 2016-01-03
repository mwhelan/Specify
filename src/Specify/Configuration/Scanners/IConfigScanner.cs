namespace Specify.Configuration.Scanners
{
    /// <summary>
    /// Scans the AppDomain to find the appropriate bootstrapper class.
    /// </summary>
    public interface IConfigScanner
    {
        /// <summary>
        /// Scans the AppDomain to find the appropriate bootstrapper class.
        /// </summary>
        /// <returns>IConfigureSpecify.</returns>
        IConfigureSpecify GetConfiguration();
    }
}