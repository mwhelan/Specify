using System;
using System.Collections.Generic;
using Specify.Mocks;
using TinyIoC;

namespace Specify.Configuration
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Inherit from this class to change any of the default configuration settings.
    /// </summary>
    public class SpecifyBootstrapper : IConfigureSpecify
    {
        /// <inheritdoc />
        public IContainer ApplicationContainer { get; internal set; }

        /// <inheritdoc />
        public List<IPerAppDomainActions> PerAppDomainActions { get; }

        /// <inheritdoc />
        public List<IPerScenarioActions> PerScenarioActions { get; }

        /// <inheritdoc />
        public bool LoggingEnabled { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecifyBootstrapper"/> class.
        /// </summary>
        public SpecifyBootstrapper()
        {
            PerAppDomainActions = new List<IPerAppDomainActions>();
            PerScenarioActions = new List<IPerScenarioActions>();
            LoggingEnabled = false;
            ApplicationContainer = BuildContainer();
        }

        /// <summary>
        /// Register any additional items into the TinyIoc container. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public virtual void ConfigureContainer(TinyIoCContainer container)
        {
            
        }

        /// <summary>
        /// Override default behaviour. By default, Specify will detect NSubstitute, FakeItEasy and Moq, in that order.
        /// If none are found, or null is returned as the mock factory, then the full TinyIoc container will be used without mocking. 
        /// </summary>
        /// <returns>Func&lt;IMockFactory&gt;.</returns>
        public virtual Func<IMockFactory> GetMockFactory()
        {
            return new MockDetector().FindAvailableMock();
        }

        private IContainer BuildContainer()
        {
            var mockFactory = GetMockFactory();
            var container = new TinyContainerFactory().Create(mockFactory);
            ConfigureContainer(container);
            return new TinyContainer(container);
        }
    }
}
