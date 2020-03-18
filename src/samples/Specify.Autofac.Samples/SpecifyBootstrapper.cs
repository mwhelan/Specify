using Autofac;
using Specify.Autofac.Samples.ChildContainers;

namespace Specify.Autofac.Samples
{
    /// <summary>
    /// The startup class to configure Specify with the Autofac container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : DefaultAutofacBootstrapper
    {

        //IN MEMORY CONFIG
        public SpecifyBootstrapper()
        {
            HtmlReport.ReportHeader = "Specify Examples";
            HtmlReport.ReportDescription = "Unit Specifications";
            HtmlReport.OutputFileName = "Specify Examples - Unit Specifications.html";
        }

        public override ContainerBuilder ConfigureContainerForApplication()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ConcreteObject>();
            builder.RegisterType<Dependency1>().As<IDependency1>();

            return builder;
        }
    }
}
