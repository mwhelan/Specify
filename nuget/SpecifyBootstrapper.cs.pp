using TinyIoC;

namespace $rootnamespace$.Specify
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : SpecifyConfiguration
    {
        /// <summary>
        /// Register any additional items into the TinyIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public virtual void ConfigureContainer(TinyIoCContainer container)
        {

        }
    }
}
