using DryIoc;
using Specify.Containers;

namespace Specify.Configuration
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Inherit from this class to change any of the default configuration settings.
    /// </summary>
    public class DefaultBootstrapper : BootstrapperBase
    {
        /// <inheritdoc />
        protected override IContainer BuildApplicationContainer()
        {
            var container = new DryContainerFactory().Create(MockFactory);
            ConfigureContainer(container);
            return new DryContainer(container);
        }

        /// <summary>
        /// Register any additional items into the DryIoc container. 
        /// </summary>
        /// <param name="container">The DryIoc <see cref="Container"/> container.</param>
        public virtual void ConfigureContainer(Container container)
        {

        }
    }
}
