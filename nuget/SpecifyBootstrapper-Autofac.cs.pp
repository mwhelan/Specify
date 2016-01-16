using Autofac;
using Specify.Configuration;

namespace $rootnamespace$.Specify
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : SpecifyConfiguration
    {
        /// <summary>
        /// Register any additional items into the Autofac container using the ContainerBuilder or leave it as it is. 
        /// </summary>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/>.</param>
        public virtual void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}
