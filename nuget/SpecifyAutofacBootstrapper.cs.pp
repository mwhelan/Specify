using Autofac;
using Specify.Autofac;

namespace $rootnamespace$
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyAutofacBootstrapper : DefaultAutofacBootstrapper
    {
        /// <summary>
        /// Register any additional items into the Autofac container using the ContainerBuilder or leave it as it is. 
        /// </summary>
        /// <param name="builder">The Autofac <see cref="ContainerBuilder"/>.</param>
        public override void ConfigureContainer(ContainerBuilder builder)
        {

        }
    }
}