using System;
using TestStack.BDDfy;
using TestStack.BDDfy.Reporters.Html;

namespace Specify.Examples.UnitSpecs.ATM
{
    /// <summary>
    /// This overrides the default html report setting
    /// </summary>
    //public class AtmHtmlReportConfig : DefaultHtmlReportConfiguration
    //{
    //    public override bool RunsOn(Story story)
    //    {
    //        return story.Namespace.EndsWith("Atm", StringComparison.OrdinalIgnoreCase);
    //    }

    //    /// <summary>
    //    /// Change the output file name
    //    /// </summary>
    //    public override string OutputFileName => "ATM.html";

    //    /// <summary>
    //    /// Change the report header to your project
    //    /// </summary>
    //    public override string ReportHeader => "ATM Solutions";

    //    /// <summary>
    //    /// Change the report description
    //    /// </summary>
    //    public override string ReportDescription => "A reliable solution for your offline banking needs";

    //    /// <summary>
    //    /// For ATM report I want to embed jQuery in the report so people can see it with no internet connectivity
    //    /// </summary>
    //    public override bool ResolveJqueryFromCdn => false;
    //}
}
