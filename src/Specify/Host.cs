using System;
using Specify.Configuration;

namespace Specify
{
    internal static class Host
    {
        private static readonly SpecifyConfiguration _configuration;

        public static readonly TestRunner SpecificationRunner;

        static Host()
        {
            var configurator = new AppConfigurator();
            _configuration = configurator.Configure();
            configurator.ConfigureBddfy(_configuration);
            SpecificationRunner = new TestRunner(_configuration.DependencyResolver(), new BddfyTestEngine());

            AppDomain.CurrentDomain.DomainUnload += CurrentDomain_DomainUnload;
            _configuration.BeforeAllTests();
        }

        static void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            _configuration.AfterAllTests();
            SpecificationRunner.Dispose();
        }
    }
}