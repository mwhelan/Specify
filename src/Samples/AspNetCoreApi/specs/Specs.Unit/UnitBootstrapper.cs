using DryIoc;
using Specify.Configuration;
using TestStack.BDDfy.Configuration;

namespace Specs.Unit.ApiTemplate
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class UnitBootstrapper : DefaultBootstrapper
    {
        public UnitBootstrapper()
        {
            HtmlReport.ReportHeader = "API Template";
            HtmlReport.ReportDescription = "Unit Specs";
            HtmlReport.OutputFileName = "ApiTemplate-UnitSpecs.html";
            Configurator.BatchProcessors.HtmlReport.Disable();
        }

        /// <summary>
        /// Register any additional items into the TinyIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="TinyIoCContainer"/> container.</param>
        public override void ConfigureContainer(Container container)
        {
           // container.Register<IPerScenarioAction, LoggingAction>();
        }
    }
}
