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
        protected override IContainer BuildApplicationContainer()
        {
            var mockFactory = GetMockFactory();
            var container = new TinyContainerFactory().Create(mockFactory);
            ConfigureContainer(container);
            return new TinyContainer(container);
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
