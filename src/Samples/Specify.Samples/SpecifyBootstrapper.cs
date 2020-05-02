﻿using System.IO;
using System.Reflection;
using DryIoc;
using Specify.Configuration;

namespace Specify.Samples
{
    /// <summary>
    /// The startup class to configure Specify with the default TinyIoc container. 
    /// Make any changes to the default configuration settings in this file.
    /// </summary>
    public class SpecifyBootstrapper : DefaultBootstrapper
    {
        public SpecifyBootstrapper()
        {
            LoggingEnabled = true;
            HtmlReport.ReportHeader = "Specify Examples";
            HtmlReport.ReportDescription = "Unit Specifications";
            HtmlReport.OutputFileName = "Specify Examples - Unit Specifications.html";
            HtmlReport.OutputPath = Path.GetDirectoryName(GetType().GetTypeInfo().Assembly.Location);

            //Log.Logger = new LoggerConfiguration()
            //    .MinimumLevel.Debug()
            //    .WriteTo.LiterateConsole()
            //    .WriteTo.RollingFile("log-{Date}.txt")
            //    .CreateLogger();
        }

        /// <summary>
        /// Register any additional items into the TinyIoc container or leave it as it is. 
        /// </summary>
        /// <param name="container">The <see cref="Container"/> container.</param>
        public override void ConfigureContainer(Container container)
        {

        }
    }
}