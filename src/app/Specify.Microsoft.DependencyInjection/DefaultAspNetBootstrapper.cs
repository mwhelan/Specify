//using Microsoft.Extensions.DependencyInjection;
//using Specify.Configuration;

//namespace Specify.Microsoft.DependencyInjection
//{
//    /// <summary>
//    /// The startup class to configure Specify with the Microsoft Dependency Injection container. 
//    /// Inherit from this class to change any of the default configuration settings.
//    /// </summary>
//    public class DefaultAspNetBootstrapper : BootstrapperBase
//    {
//        /// <inheritdoc />
//        protected override IContainer BuildApplicationContainer()
//        {
//            var container = new DryContainerFactory().Create(MockFactory);
//            ConfigureContainer(null);
//            return new DryContainer(container);
//        }

//        /// <summary>
//        /// Register any additional items into the Microsoft Dependency Injection container. 
//        /// </summary>
//        /// <param name="services">The <see cref="IServiceCollection"/> container.</param>
//        public virtual void ConfigureContainer(IServiceCollection services)
//        {

//        }
//    }
//}
