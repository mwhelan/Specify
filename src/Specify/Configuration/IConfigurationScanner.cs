namespace Specify.Configuration
{
    /// <summary>
    /// Scans the AppDomain to find the appropriate bootstrapper class.
    /// </summary>
    public interface IConfigurationScanner
    {
        /// <summary>
        /// Scans the AppDomain to find the appropriate bootstrapper class.
        /// </summary>
        /// <returns>IConfigureSpecify.</returns>
        IConfigureSpecify GetConfiguration();
    }
}