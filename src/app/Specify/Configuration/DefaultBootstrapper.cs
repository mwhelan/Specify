using TinyIoC;

namespace Specify.Configuration
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Inherit from this class to change any of the default configuration settings.
    /// </summary>
    public class DefaultBootstrapper : BootstrapperBase
    {
        /// <inheritdoc />
        protected override IContainerRoot BuildApplicationContainer()
        {
            var tinyContainer = new TinyContainerFactory().Create(MockFactory);
            ConfigureContainer(tinyContainer);
            var containerRoot = tinyContainer.Resolve<IContainerRoot>();
            return containerRoot;
        }

        /// <summary>
        /// Register any additional items into the TinyIoc container. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public virtual void ConfigureContainer(TinyIoCContainer container)
        {

        }
    }
}
