using TestStack.BDDfy.Configuration;
using TestStack.BDDfy.Processors.Reporters.Html;

namespace Specify
{
    public static class SpecifyConfiguration
    {
        public static void InitializeSpecify()
        {
            Configurator.BatchProcessors.HtmlReport.Disable();
            Configurator.BatchProcessors.Add(new HtmlReporter(new SelenoDesignSpecsHtmlReportConfig()));
            Configurator.Scanners.StoryMetaDataScanner = () => new SpecStoryMetaDataScanner();
        }
    }
}
